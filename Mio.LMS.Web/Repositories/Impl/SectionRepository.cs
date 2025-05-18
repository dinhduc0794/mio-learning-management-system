using Microsoft.EntityFrameworkCore;
using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories.Impl;

public class SectionRepository : GenericRepository<Section>, ISectionRepository
{
    public SectionRepository(LMSDbContext context) : base(context) { }
    public async Task<List<Section>> GetAllWithLessonsAsync(int courseId)
    {
        return await _context.Sections
            .Include(s => s.Lessons)
            .Where(s => s.CourseId == courseId && !s.IsDeleted)
            .ToListAsync();
    }

    public async Task<Section> GetByIdWithLessonsAsync(int sectionId)
    {
        return await _context.Sections
            .Include(s => s.Lessons)
            .FirstOrDefaultAsync(s => s.SectionId == sectionId && !s.IsDeleted);
    }
}