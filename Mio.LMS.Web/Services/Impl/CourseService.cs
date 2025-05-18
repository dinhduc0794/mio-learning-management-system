using Mio.LMS.Web.Models;
using Mio.LMS.Web.Models.ViewModels;
using Mio.LMS.Web.UnitOfWorks;

namespace Mio.LMS.Web.Services.Impl;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<List<CourseViewModel>>> GetAllCoursesAsync()
    {
        var courses = await _unitOfWork.Courses.GetAllAsync();
        var result = courses.Select(c => new CourseViewModel
        {
            CourseId = c.CourseId,
            CourseName = c.CourseName,
            Description = c.Description,
            CategoryId = c.CategoryId,
            Category = c.Category,
            ImageUrl = c.ImageUrl,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            IsActive = c.IsActive,
            IsDeleted = c.IsDeleted,
            CreatedByUserId = c.CreatedByUserId,
            UpdatedByUserId = c.UpdatedByUserId
        }).ToList();

        return ResultViewModel<List<CourseViewModel>>.Success(result, "Lấy danh sách khóa học thành công");
    }

    public async Task<ResultViewModel<CourseViewModel>> GetCourseByIdAsync(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course == null)
            return ResultViewModel<CourseViewModel>.Failure("Không tìm thấy khóa học");

        var model = new CourseViewModel
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Description = course.Description,
            CategoryId = course.CategoryId,
            ImageUrl = course.ImageUrl,
            CreatedAt = course.CreatedAt,
            UpdatedAt = course.UpdatedAt,
            IsActive = course.IsActive,
            IsDeleted = course.IsDeleted,
            CreatedByUserId = course.CreatedByUserId,
            UpdatedByUserId = course.UpdatedByUserId
        };

        return ResultViewModel<CourseViewModel>.Success(model, "Lấy chi tiết khóa học thành công");
    }

    public async Task<ResultViewModel<CourseViewModel>> AddCourseAsync(CourseViewModel model)
    {
        var course = new Course
        {
            CourseName = model.CourseName,
            Description = model.Description,
            CategoryId = model.CategoryId,
            ImageUrl = model.ImageUrl,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsActive = true,
            IsDeleted = false
        };

        await _unitOfWork.Courses.AddAsync(course);
        await _unitOfWork.CompleteAsync();

        model.CourseId = course.CourseId;
        model.CreatedAt = course.CreatedAt;

        return ResultViewModel<CourseViewModel>.Success(model, "Tạo khóa học thành công");
    }

    public async Task<ResultViewModel<CourseViewModel>> UpdateCourseAsync(CourseViewModel model)
    {
        var existing = await _unitOfWork.Courses.GetByIdAsync(model.CourseId);
        if (existing == null)
            return ResultViewModel<CourseViewModel>.Failure("Không tìm thấy khóa học");

        existing.CourseName = model.CourseName;
        existing.Description = model.Description;
        existing.CategoryId = model.CategoryId;
        existing.ImageUrl = model.ImageUrl;
        existing.UpdatedAt = DateTime.Now;
        existing.IsActive = model.IsActive;
        existing.IsDeleted = model.IsDeleted;
        existing.UpdatedByUserId = model.UpdatedByUserId;

        await _unitOfWork.CompleteAsync();

        return ResultViewModel<CourseViewModel>.Success(model, "Cập nhật khóa học thành công");
    }

    public async Task<ResultViewModel<bool>> DeleteCourseAsync(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course == null)
            return ResultViewModel<bool>.Failure("Không tìm thấy khóa học");

        _unitOfWork.Courses.Remove(course);
        await _unitOfWork.CompleteAsync();

        return ResultViewModel<bool>.Success(true, "Xóa khóa học thành công");
    }
}
