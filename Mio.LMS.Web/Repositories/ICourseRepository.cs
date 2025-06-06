using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<List<Course>> GetAllWithCategoryAsync();
    Task<Course> GetByIdWithSectionsAndLessonsAsync(int id);
}