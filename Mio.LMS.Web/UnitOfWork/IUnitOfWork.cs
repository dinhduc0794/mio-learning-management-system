using Mio.LMS.Web.Repositories;

namespace Mio.LMS.Web.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ICourseRepository Courses { get; }
    int Complete();
}