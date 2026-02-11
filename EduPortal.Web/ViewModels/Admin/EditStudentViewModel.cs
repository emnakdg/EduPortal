using System.ComponentModel.DataAnnotations;

namespace EduPortal.Web.ViewModels.Admin;

public class EditStudentViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad Soyad zorunludur.")]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Öğrenci No zorunludur.")]
    [Display(Name = "Öğrenci No")]
    public string StudentNumber { get; set; } = string.Empty;

    [Display(Name = "Sınıf")]
    public int? ClassId { get; set; }
}