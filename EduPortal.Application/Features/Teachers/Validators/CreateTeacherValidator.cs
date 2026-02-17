using FluentValidation;
using EduPortal.Application.Features.Teachers.Commands;

namespace EduPortal.Application.Features.Teachers.Validators;

public class CreateTeacherValidator : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Öğretmen adı boş olamaz.")
            .MaximumLength(100).WithMessage("Ad en fazla 100 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

        RuleFor(x => x.Branch)
            .NotEmpty().WithMessage("Branş boş olamaz.");
    }
}