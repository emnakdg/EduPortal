using MediatR;

namespace EduPortal.Application.Features.Assignments.Commands;

public record SubmitAssignmentCommand(
    int StudentAssignmentId,
    int CorrectAnswers,
    int WrongAnswers,
    int EmptyAnswers
) : IRequest<bool>;
