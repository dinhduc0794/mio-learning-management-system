using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Services;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseByIdAsync(int id);
    Task AddCourseAsync(Course course);
    Task<bool> DeleteCourseAsync(int id);
}