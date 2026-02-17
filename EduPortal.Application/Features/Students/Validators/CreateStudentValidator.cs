using FluentValidation;
using EduPortal.Application.Features.Students.Commands;

namespace EduPortal.Application.Features.Students.Validators;

public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Öğrenci adı boş olamaz.")
            .MaximumLength(100).WithMessage("Ad en fazla 100 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

        RuleFor(x => x.StudentNumber)
            .NotEmpty().WithMessage("Öğrenci numarası boş olamaz.");
    }
}