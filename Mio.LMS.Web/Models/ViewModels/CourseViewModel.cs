namespace Mio.LMS.Web.Models.ViewModels;

public class CourseViewModel : Course
{
    public int RecordCount { get; set; }
    public bool IsEdit { get; set; }
    public bool IsSuccess { get; set; }
}