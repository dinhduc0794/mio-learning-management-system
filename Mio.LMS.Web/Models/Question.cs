using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mio.LMS.Web.Models;

public class Question : BaseModel
{
    [Key]
    public int QuestionId { get; set; }
    [Required, MaxLength(1000)]
    public string Content { get; set; }
    public int QuizId { get; set; }
    [ForeignKey("QuizId")]
    public Quiz Quiz { get; set; }
    public ICollection<Answer> Answers { get; set; }
    public ICollection<UserAnswer> UserAnswers { get; set; }
}
