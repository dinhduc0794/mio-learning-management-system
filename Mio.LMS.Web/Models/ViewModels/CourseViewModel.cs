using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Mio.LMS.Web.Models.ViewModels;

public class CourseViewModel : BaseModel
{
    public int CourseId { get; set; }
    [Required(ErrorMessage = "Tên khóa học là bắt buộc")]
    [MaxLength(100)]
    public string CourseName { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    public int? CategoryId { get; set; }

    [ValidateNever]
    public string? CategoryName { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    
    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public int RecordCount { get; set; }
    public bool IsEdit { get; set; }
    public bool IsSuccess { get; set; }

    public List<SectionViewModel> Sections { get; set; } = new List<SectionViewModel>();
    public List<AssignmentViewModel> Assignments { get; set; } = new List<AssignmentViewModel>();
}