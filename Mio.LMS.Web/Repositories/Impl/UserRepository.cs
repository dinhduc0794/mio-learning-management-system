using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories.Impl;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(LMSDbContext context) : base(context)
    {
    }
}
