using MediatR;

namespace EduPortal.Application.Features.Students.Commands;

public record CreateStudentCommand(
    string FullName,
    string Email,
    string Password,
    string StudentNumber,
    int? ClassId
) : IRequest<int>;