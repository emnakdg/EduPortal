using MediatR;

namespace EduPortal.Application.Features.TeacherNotes.Commands;

public record DeleteTeacherNoteCommand(int Id) : IRequest<bool>;
