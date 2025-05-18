using Microsoft.AspNetCore.Mvc;
using Mio.LMS.Web.Services;

namespace Mio.LMS.Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }
    
    public IActionResult Index()
    {
        return View();
    }
}