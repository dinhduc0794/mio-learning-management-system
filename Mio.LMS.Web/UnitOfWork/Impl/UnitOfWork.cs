using Mio.LMS.Web.Models;
using Mio.LMS.Web.Repositories;
using Mio.LMS.Web.Repositories.Impl;

namespace Mio.LMS.Web.UnitOfWorks.Impl;

public class UnitOfWork : IUnitOfWork
{
    private readonly LMSDbContext _context;
    public IUserRepository Users { get; private set; }
    public ICourseRepository Courses { get; private set; }
    public ISectionRepository Sections { get; private set; }
    public ILessonRepository Lessons { get; private set; }

    public UnitOfWork(LMSDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);   
        Courses = new CourseRepository(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}