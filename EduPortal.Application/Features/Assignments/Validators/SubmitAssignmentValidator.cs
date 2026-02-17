using FluentValidation;
using EduPortal.Application.Features.Assignments.Commands;

namespace EduPortal.Application.Features.Assignments.Validators;

public class SubmitAssignmentValidator : AbstractValidator<SubmitAssignmentCommand>
{
    public SubmitAssignmentValidator()
    {
        RuleFor(x => x.StudentAssignmentId)
            .GreaterThan(0).WithMessage("Geçersiz ödev kaydı.");

        RuleFor(x => x.CorrectAnswers)
            .GreaterThanOrEqualTo(0).WithMessage("Doğru sayısı negatif olamaz.");

        RuleFor(x => x.WrongAnswers)
            .GreaterThanOrEqualTo(0).WithMessage("Yanlış sayısı negatif olamaz.");

        RuleFor(x => x.EmptyAnswers)
            .GreaterThanOrEqualTo(0).WithMessage("Boş sayısı negatif olamaz.");
    }
}