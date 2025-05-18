using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories.Impl;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(LMSDbContext context) : base(context) { }
}
