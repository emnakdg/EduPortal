using EduPortal.Domain.Enums;
using MediatR;

namespace EduPortal.Application.Features.Assignments.Commands;

public record UpdateAssignmentCommand(
    int Id,
    string Title,
    string? Description,
    int QuestionCount,
    DateTime DueDate,
    AssignmentPriority Priority
) : IRequest<bool>;
