using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mio.LMS.Web.Models;

public abstract class BaseModel
{
    
    public BaseModel()
    {
        this.UpdatedAt = DateTime.Now;
        this.CreatedAt = DateTime.Now;
    }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [ForeignKey("CreatedByUserId")]
    public User? CreatedByUser { get; set; }

    public int? UpdatedByUserId { get; set; }
    
    [ForeignKey("UpdatedByUserId")]
    public User? UpdatedByUser { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;

    [DefaultValue(false)]
    public bool IsDeleted { get; set; } = false;
}