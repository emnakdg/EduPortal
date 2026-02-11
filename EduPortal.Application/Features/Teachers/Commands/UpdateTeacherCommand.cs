using MediatR;

namespace EduPortal.Application.Features.Teachers.Commands;

public record UpdateTeacherCommand(
    int Id,
    string FullName,
    string Branch
) : IRequest<bool>;