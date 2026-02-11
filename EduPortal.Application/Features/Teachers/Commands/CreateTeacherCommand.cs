using MediatR;

namespace EduPortal.Application.Features.Teachers.Commands;

public record CreateTeacherCommand(
    string FullName,
    string Email,
    string Password,
    string Branch
) : IRequest<int>;