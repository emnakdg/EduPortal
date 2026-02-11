using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Subjects.Queries;

public record GetAllSubjectsQuery() : IRequest<List<SubjectDto>>;