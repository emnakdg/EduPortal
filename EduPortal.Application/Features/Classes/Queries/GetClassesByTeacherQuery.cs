using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Classes.Queries;

public record GetClassesByTeacherQuery(int TeacherId) : IRequest<List<ClassDto>>;
