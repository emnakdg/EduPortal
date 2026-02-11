using MediatR;

namespace EduPortal.Application.Features.Assignments.Commands;

public record DeleteAssignmentCommand(int Id) : IRequest<bool>;