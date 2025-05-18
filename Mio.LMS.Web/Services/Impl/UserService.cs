using Mio.LMS.Web.UnitOfWorks;

namespace Mio.LMS.Web.Services.Impl;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}