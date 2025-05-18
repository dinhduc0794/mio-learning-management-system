using Mio.LMS.Web.Models;
using Mio.LMS.Web.UnitOfWorks;

namespace Mio.LMS.Web.Services.Impl;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _unitOfWork.Courses.GetAllAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _unitOfWork.Courses.GetByIdAsync(id);
    }

    public async Task AddCourseAsync(Course course)
    {
        await _unitOfWork.Courses.AddAsync(course);
        await Task.Run(() => _unitOfWork.Complete());
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course == null) return false;

        _unitOfWork.Courses.Remove(course);
        await Task.Run(() => _unitOfWork.Complete());
        return true;
    }
}