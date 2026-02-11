using MediatR;

namespace EduPortal.Application.Features.Classes.Commands;

public record UpdateClassCommand(
    int Id,
    string Name,
    string Semester,
    string? Room,
    int TeacherId
) : IRequest<bool>;