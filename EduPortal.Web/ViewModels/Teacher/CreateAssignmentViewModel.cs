using EduPortal.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EduPortal.Web.ViewModels.Teacher;

public class CreateAssignmentViewModel
{
    [Required(ErrorMessage = "Ödev başlığı zorunludur.")]
    [Display(Name = "Ödev Başlığı")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Soru sayısı zorunludur.")]
    [Range(1, 100)]
    [Display(Name = "Soru Sayısı")]
    public int QuestionCount { get; set; }

    [Required(ErrorMessage = "Son teslim tarihi zorunludur.")]
    [Display(Name = "Son Teslim Tarihi")]
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);

    [Display(Name = "Öncelik")]
    public AssignmentPriority Priority { get; set; } = AssignmentPriority.Medium;

    [Required(ErrorMessage = "Konu seçiniz.")]
    [Display(Name = "Konu")]
    public int TopicId { get; set; }

    [Display(Name = "Öğrenciler")]
    public List<int>? StudentIds { get; set; }
}