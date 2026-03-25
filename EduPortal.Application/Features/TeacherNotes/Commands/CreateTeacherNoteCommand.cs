using MediatR;

namespace EduPortal.Application.Features.TeacherNotes.Commands;

public record CreateTeacherNoteCommand(string Content, int TeacherId, int StudentId) : IRequest<int>;
