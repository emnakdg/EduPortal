using System.ComponentModel.DataAnnotations;

namespace EduPortal.Web.ViewModels.Admin;

public class EditTeacherViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad Soyad zorunludur.")]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Branş zorunludur.")]
    [Display(Name = "Branş")]
    public string Branch { get; set; } = string.Empty;
}