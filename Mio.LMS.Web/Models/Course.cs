using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mio.LMS.Web.Models;

public class Course : BaseModel
{
    [Key]
    public int CourseId { get; set; }
    [Required, MaxLength(100)]
    public string CourseName { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
    
    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")] 
    public Category? Category { get; set; }
    
    [MaxLength(500)]
    public string ImageUrl { get; set; }
    public int Status { get; set; } // 0: Draft, 1: Published, 2: Archived

    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<Section> Sections { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
    public ICollection<Assignment> Assignments { get; set; }
    
}