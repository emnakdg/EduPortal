using MediatR;

namespace EduPortal.Application.Features.Subjects.Commands;

public record DeleteSubjectCommand(int Id) : IRequest<bool>;