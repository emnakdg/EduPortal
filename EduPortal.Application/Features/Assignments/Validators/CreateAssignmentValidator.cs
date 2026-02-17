using FluentValidation;
using EduPortal.Application.Features.Assignments.Commands;

namespace EduPortal.Application.Features.Assignments.Validators;

public class CreateAssignmentValidator : AbstractValidator<CreateAssignmentCommand>
{
    public CreateAssignmentValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Ödev başlığı boş olamaz.")
            .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

        RuleFor(x => x.QuestionCount)
            .GreaterThan(0).WithMessage("Soru sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100).WithMessage("Soru sayısı en fazla 100 olabilir.");

        RuleFor(x => x.DueDate)
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Son tarih bugün veya ileri bir tarih olmalıdır.");

        RuleFor(x => x.TopicId)
            .GreaterThan(0).WithMessage("Konu seçilmelidir.");

        RuleFor(x => x.StudentIds)
            .NotEmpty().WithMessage("En az bir öğrenci seçilmelidir.");
    }
}