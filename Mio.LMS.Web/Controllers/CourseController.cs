using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mio.LMS.Web.Models;
using Mio.LMS.Web.Models.ViewModels;
using Mio.LMS.Web.Services;
using System.Threading.Tasks;

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
        var result = await _courseService.GetAllCoursesAsync(categoryId);
        if (!result.IsSuccess)
        {
            ViewBag.ErrorMessage = result.Message;
            return View(new List<CourseViewModel>());
        }

        ViewBag.AllCategories = await GetCategorySelectList();
        ViewBag.SelectedCategoryId = categoryId;

        return View(result.Data ?? new List<CourseViewModel>());
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses(int? categoryId)
    {
        var result = await _courseService.GetAllCoursesAsync(categoryId);
        if (!result.IsSuccess)
        {
            return Json(new { success = false, message = result.Message });
        }

        return Json(new { success = true, message = result.Message, data = result.Data ?? new List<CourseViewModel>() });
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
            var errorDetails = ModelState
                .Select(kvp => new {
                    Field = kvp.Key,
                    Errors = kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                }).Where(e => e.Errors.Any())
                .ToList();

            return Json(new {
                success = false,
                message = "Dữ liệu không hợp lệ",
                errors = errorDetails
            });
        }

        var result = await _courseService.AddCourseAsync(model);
        if (!result.IsSuccess)
        {
            return Json(new { success = false, message = result.Message });
        }

        return Json(new { success = true, message = result.Message });
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
            var errorDetails = ModelState
                .Select(kvp => new {
                    Field = kvp.Key,
                    Errors = kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                }).Where(e => e.Errors.Any())
                .ToList();

            return Json(new {
                success = false,
                message = "Dữ liệu không hợp lệ",
                errors = errorDetails
            });
        }

        var result = await _courseService.UpdateCourseAsync(model);
        if (!result.IsSuccess)
        {
            return Json(new { success = false, message = result.Message });
        }

        return Json(new { success = true, message = result.Message });
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