using System.ComponentModel.DataAnnotations;

namespace EduPortal.Web.ViewModels.Admin;

public class CreateClassViewModel
{
    [Required(ErrorMessage = "Sınıf adı zorunludur.")]
    [Display(Name = "Sınıf Adı")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dönem zorunludur.")]
    [Display(Name = "Dönem")]
    public string Semester { get; set; } = string.Empty;

    [Display(Name = "Derslik")]
    public string? Room { get; set; }

    [Required(ErrorMessage = "Sınıf sorumlusu seçiniz.")]
    [Display(Name = "Sorumlu Öğretmen")]
    public int TeacherId { get; set; }
}