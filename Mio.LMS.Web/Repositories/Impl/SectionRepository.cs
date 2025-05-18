using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories.Impl;

public class SectionRepository : GenericRepository<Section>, ISectionRepository
{
    public SectionRepository(LMSDbContext context) : base(context) { }
}

