using MediatR;

namespace EduPortal.Application.Features.Classes.Commands;

public record DeleteClassCommand(int Id) : IRequest<bool>;