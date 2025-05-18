using Microsoft.AspNetCore.Mvc;
using Mio.LMS.Web.Models;
using Mio.LMS.Web.Services;

namespace Mio.LMS.Web.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return View(courses);
    }

    public async Task<IActionResult> Details(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null) return NotFound();

        return View(course);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        if (!ModelState.IsValid)
            return View(course);

        await _courseService.AddCourseAsync(course);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _courseService.DeleteCourseAsync(id);
        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}