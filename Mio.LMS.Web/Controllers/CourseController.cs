using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mio.LMS.Web.Models;
using Mio.LMS.Web.Models.ViewModels;
using Mio.LMS.Web.Services;

namespace Mio.LMS.Web.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ICategoryService _categoryService;

    public CourseController(ICourseService courseService, ICategoryService categoryService)
    {
        _courseService = courseService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(int? categoryId)
    {
        var result = await _courseService.GetAllCoursesAsync();
        if (!result.IsSuccess)
        {
            ViewBag.ErrorMessage = result.Message;
            return View(new List<CourseViewModel>());
        }

        var courses = result.Data;
        if (categoryId.HasValue)
            courses = courses.Where(c => c.CategoryId == categoryId).ToList();

        ViewBag.AllCategories = await GetCategorySelectList();
        ViewBag.SelectedCategoryId = categoryId;

        return View(courses);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.AllCategories = await GetCategorySelectList();
        return View("Form", new CourseViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.AllCategories = await GetCategorySelectList();
            return View("Form", model);
        }

        var result = await _courseService.AddCourseAsync(model);
        if (!result.IsSuccess)
        {
            ViewBag.AllCategories = await GetCategorySelectList();
            ModelState.AddModelError("", result.Message);
            return View("Form", model);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.AllCategories = await GetCategorySelectList();
        var result = await _courseService.GetCourseByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound();

        return View("Form", result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.AllCategories = await GetCategorySelectList();
            return View("Form", model);
        }

        var result = await _courseService.UpdateCourseAsync(model);
        if (!result.IsSuccess)
        {
            ViewBag.AllCategories = await GetCategorySelectList();
            ModelState.AddModelError("", result.Message);
            return View("Form", model);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _courseService.DeleteCourseAsync(id);
        return Json(new { success = result.IsSuccess, message = result.Message });
    }

    private async Task<List<SelectListItem>> GetCategorySelectList()
    {
        var result = await _categoryService.GetAllCategoriesAsync();

        if (!result.IsSuccess || result.Data == null)
            return new List<SelectListItem>();

        return result.Data.Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(),
            Text = c.Name
        }).ToList();
    }
}