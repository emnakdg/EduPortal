using MediatR;

namespace EduPortal.Application.Features.Students.Commands;

public record UpdateStudentCommand(
    int Id,
    string FullName,
    string StudentNumber,
    int? ClassId
) : IRequest<bool>;