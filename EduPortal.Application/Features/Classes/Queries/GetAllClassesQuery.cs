using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Classes.Queries;

public record GetAllClassesQuery() : IRequest<List<ClassDto>>;