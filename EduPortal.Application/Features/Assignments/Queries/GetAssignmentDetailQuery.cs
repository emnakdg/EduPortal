using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Assignments.Queries;

public record GetAssignmentDetailQuery(int AssignmentId) : IRequest<AssignmentDetailDto?>;