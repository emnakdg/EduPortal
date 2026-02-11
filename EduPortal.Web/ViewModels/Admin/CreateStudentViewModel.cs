using System.ComponentModel.DataAnnotations;

namespace EduPortal.Web.ViewModels.Admin;

public class CreateStudentViewModel
{
    [Required(ErrorMessage = "Ad Soyad zorunludur.")]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [MinLength(6)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Öğrenci No zorunludur.")]
    [Display(Name = "Öğrenci No")]
    public string StudentNumber { get; set; } = string.Empty;

    [Display(Name = "Sınıf")]
    public int? ClassId { get; set; }
}