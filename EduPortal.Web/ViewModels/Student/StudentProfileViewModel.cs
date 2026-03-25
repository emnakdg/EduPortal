using System.ComponentModel.DataAnnotations;

namespace EduPortal.Web.ViewModels.Student;

public class StudentProfileViewModel
{
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Öğrenci Numarası")]
    public string StudentNumber { get; set; } = string.Empty;

    [Display(Name = "Sınıf")]
    public string? ClassName { get; set; }

    [Display(Name = "Tamamlanan Ödev")]
    public int CompletedAssignments { get; set; }

    [Display(Name = "Bekleyen Ödev")]
    public int PendingAssignments { get; set; }

    [Display(Name = "Başarı Oranı")]
    public double SuccessRate { get; set; }
}

public class StudentChangePasswordViewModel
{
    [Required(ErrorMessage = "Mevcut şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mevcut Şifre")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni şifre zorunludur.")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    [DataType(DataType.Password)]
    [Display(Name = "Yeni Şifre")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
    [DataType(DataType.Password)]
    [Display(Name = "Yeni Şifre Tekrar")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
