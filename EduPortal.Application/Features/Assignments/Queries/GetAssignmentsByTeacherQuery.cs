using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Assignments.Queries;

public record GetAssignmentsByTeacherQuery(int TeacherId) : IRequest<List<AssignmentDto>>;