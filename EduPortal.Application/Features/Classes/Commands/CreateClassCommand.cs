using MediatR;

namespace EduPortal.Application.Features.Classes.Commands;

public record CreateClassCommand(
    string Name,
    string Semester,
    string? Room,
    int TeacherId
) : IRequest<int>;