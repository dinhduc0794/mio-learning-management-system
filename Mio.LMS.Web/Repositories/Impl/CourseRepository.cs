using Microsoft.EntityFrameworkCore;
using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories.Impl;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(LMSDbContext context) : base(context) { }
    
    public async Task<List<Course>> GetAllWithCategoryAsync()
    {
        return await _context.Courses
            .Include(c => c.Category)
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }
}
