using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.TeacherNotes.Queries;

public record GetNotesByTeacherQuery(int TeacherId) : IRequest<List<TeacherNoteDto>>;
