using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Teachers.Queries;

public record GetAllTeachersQuery() : IRequest<List<TeacherDto>>;