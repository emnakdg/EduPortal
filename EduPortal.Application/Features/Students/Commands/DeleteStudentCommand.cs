using MediatR;

namespace EduPortal.Application.Features.Students.Commands;

public record DeleteStudentCommand(int Id) : IRequest<bool>;