using Mio.LMS.Web.Models;
using Mio.LMS.Web.Models.ViewModels;

namespace Mio.LMS.Web.Services;

public interface ICourseService
{
    Task<ResultViewModel<List<CourseViewModel>>> GetAllCoursesAsync();
    Task<ResultViewModel<CourseViewModel>> GetCourseByIdAsync(int id);
    Task<ResultViewModel<CourseViewModel>> AddCourseAsync(CourseViewModel courseViewModel);
    Task<ResultViewModel<CourseViewModel>> UpdateCourseAsync(CourseViewModel courseViewModel);
    Task<ResultViewModel<bool>> DeleteCourseAsync(int id);
}