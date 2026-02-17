using MediatR;

namespace EduPortal.Application.Features.Assignments.Commands;

public record GiveFeedbackCommand(
    int StudentAssignmentId,
    string Feedback
) : IRequest<bool>;