// using Microsoft.AspNetCore.Mvc;
// using Mio.LMS.Web.Models.ViewModels;
// using Mio.LMS.Web.Services;
//
// namespace Mio.LMS.Web.Controllers;
//
// public class SectionController : Controller
// {
//     private readonly ISectionService _sectionService;
//     public SectionController(ISectionService sectionService)
//     {
//         _sectionService = sectionService;
//     }
//
//     [HttpGet]
//     public async Task<IActionResult> Index(int courseId)
//     {
//         var result = await _sectionService.GetAllSectionsAsync(courseId);
//         if (!result.IsSuccess)
//         {
//             ViewBag.ErrorMessage = result.Message;
//             return View(new List<SectionViewModel>());
//         }
//
//         ViewBag.CourseId = courseId;
//         return View(result.Data ?? new List<SectionViewModel>());
//     }
//
//     [HttpGet]
//     public IActionResult Create(int courseId)
//     {
//         return View("Form", new SectionViewModel() { CourseId = courseId });
//     }
//
//     [HttpPost]
//     public async Task<IActionResult> Create(SectionViewModel model)
//     {
//         if (!ModelState.IsValid)
//         {
//             return View("Form", model);
//         }
//
//         var result = await _sectionService.AddSectionAsync(model);
//         if (!result.IsSuccess)
//         {
//             ModelState.AddModelError("", result.Message);
//             return View("Form", model);
//         }
//
//         return RedirectToAction("Index", new { courseId = model.CourseId });
//     }
//
//     [HttpGet]
//     public async Task<IActionResult> Edit(int id)
//     {
//         var result = await _sectionService.GetSectionByIdAsync(id);
//         if (!result.IsSuccess)
//             return NotFound();
//
//         return View("Form", result.Data);
//     }
//
//     [HttpPost]
//     public async Task<IActionResult> Edit(SectionViewModel model)
//     {
//         if (!ModelState.IsValid)
//         {
//             return View("Form", model);
//         }
//
//         var result = await _sectionService.UpdateSectionAsync(model);
//         if (!result.IsSuccess)
//         {
//             ModelState.AddModelError("", result.Message);
//             return View("Form", model);
//         }
//
//         return RedirectToAction("Index", new { courseId = model.CourseId });
//     }
//
//     [HttpPost]
//     public async Task<IActionResult> Delete(int id)
//     {
//         var result = await _sectionService.DeleteSectionAsync(id);
//         return Json(new { success = result.IsSuccess, message = result.Message });
//     }
// }