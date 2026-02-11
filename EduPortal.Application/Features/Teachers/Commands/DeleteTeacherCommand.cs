using MediatR;

namespace EduPortal.Application.Features.Teachers.Commands;

public record DeleteTeacherCommand(int Id) : IRequest<bool>;