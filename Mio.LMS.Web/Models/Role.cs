using System.ComponentModel.DataAnnotations;

namespace Mio.LMS.Web.Models;

public class Role
{
    [Key]
    public int RoleId { get; set; }

    [Required, MaxLength(50)]
    public string RoleName { get; set; } // e.g., "Student", "Teacher", "Admin"

    public ICollection<User> Users { get; set; }
}