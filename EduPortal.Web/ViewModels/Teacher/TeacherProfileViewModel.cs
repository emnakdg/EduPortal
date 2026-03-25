using System.ComponentModel.DataAnnotations;

namespace EduPortal.Web.ViewModels.Teacher;

public class TeacherProfileViewModel
{
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Branş")]
    public string Branch { get; set; } = string.Empty;

    [Display(Name = "Sınıf Sayısı")]
    public int ClassCount { get; set; }

    [Display(Name = "Öğrenci Sayısı")]
    public int StudentCount { get; set; }
}

public class ChangePasswordViewModel
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
