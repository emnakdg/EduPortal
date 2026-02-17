using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Assignments.Queries;

public record GetStudentAssignmentsQuery(int StudentId) : IRequest<List<StudentAssignmentDto>>;
