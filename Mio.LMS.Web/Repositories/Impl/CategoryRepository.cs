using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories.Impl;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(LMSDbContext context) : base(context) { }
}