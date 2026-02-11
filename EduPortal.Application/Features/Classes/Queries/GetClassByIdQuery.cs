using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Classes.Queries;

public record GetClassByIdQuery(int Id) : IRequest<ClassDto?>;