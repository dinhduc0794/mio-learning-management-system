using Mio.LMS.Web.Repositories;

namespace Mio.LMS.Web.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ICourseRepository Courses { get; }
    ICategoryRepository Categories { get; }
    ISectionRepository Sections { get; }
    ILessonRepository Lessons { get; }
    int Complete();
    Task<int> CompleteAsync();
}