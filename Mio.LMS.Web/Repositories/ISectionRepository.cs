using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories;

public interface ISectionRepository : IGenericRepository<Section>
{
    void RemoveRange(IEnumerable<Section> sections);
    Task<List<Section>> GetAllWithLessonsAsync(int courseId);
    Task<Section> GetByIdWithLessonsAsync(int sectionId);
}