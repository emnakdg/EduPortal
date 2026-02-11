using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Subjects.Queries;

public record GetSubjectByIdQuery(int Id) : IRequest<SubjectDto?>;