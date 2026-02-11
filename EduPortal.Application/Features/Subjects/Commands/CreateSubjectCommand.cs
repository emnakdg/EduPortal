using MediatR;

namespace EduPortal.Application.Features.Subjects.Commands;

public record CreateSubjectCommand(
    string Name,
    string Code,
    string? Color,
    int GradeLevel
) : IRequest<int>;