using FluentValidation;
using EduPortal.Application.Features.Assignments.Commands;

namespace EduPortal.Application.Features.Assignments.Validators;

public class GiveFeedbackValidator : AbstractValidator<GiveFeedbackCommand>
{
    public GiveFeedbackValidator()
    {
        RuleFor(x => x.StudentAssignmentId)
            .GreaterThan(0).WithMessage("Geçersiz ödev kaydı.");

        RuleFor(x => x.Feedback)
            .NotEmpty().WithMessage("Geri bildirim boş olamaz.")
            .MaximumLength(500).WithMessage("Geri bildirim en fazla 500 karakter olabilir.");
    }
}