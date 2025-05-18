﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mio.LMS.Web.Models;

public class Answer : BaseModel
{
    [Key]
    public int AnswerId { get; set; }
    [Required, MaxLength(1000)]
    public string Content { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
    [ForeignKey("QuestionId")]
    public Question Question { get; set; }
}