using EduPortal.Domain.Enums;
using MediatR;

namespace EduPortal.Application.Features.Assignments.Commands;

public record CreateAssignmentCommand(
    string Title,
    string? Description,
    int QuestionCount,
    DateTime DueDate,
    AssignmentPriority Priority,
    int TeacherId,
    int TopicId,
    List<int> StudentIds
) : IRequest<int>;