using System.ComponentModel.DataAnnotations;

namespace Mio.LMS.Web.Models;

public class Category : BaseModel
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}