using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories.Impl;

public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
{
    public LessonRepository(LMSDbContext context) : base(context) { }
}