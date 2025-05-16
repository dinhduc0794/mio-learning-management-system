using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mio.LMS.Web.Models;

public class User : BaseModel
{
    [Key]
    public int UserId { get; set; }

    public int RoleId { get; set; }
    [ForeignKey("RoleId")] public Role Role { get; set; }
    
    [Required, MaxLength(50)]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }

    [MaxLength(15)]
    public string? PhoneNumber { get; set; }    
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Required, MaxLength(100)]
    public string FullName { get; set; }

    public DateTime? Dob { get; set; }
    
    public string? Address { get; set; }

    public string? Image { get; set; }
}