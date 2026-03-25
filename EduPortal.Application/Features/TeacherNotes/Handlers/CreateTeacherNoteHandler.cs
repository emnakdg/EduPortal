using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.TeacherNotes.Commands;
using EduPortal.Domain.Entities;
using MediatR;

namespace EduPortal.Application.Features.TeacherNotes.Handlers;

public class CreateTeacherNoteHandler : IRequestHandler<CreateTeacherNoteCommand, int>
{
    private readonly IEduPortalDbContext _context;

    public CreateTeacherNoteHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTeacherNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new TeacherNote
        {
            Content = request.Content,
            TeacherId = request.TeacherId,
            StudentId = request.StudentId
        };

        _context.TeacherNotes.Add(note);
        await _context.SaveChangesAsync(cancellationToken);
        return note.Id;
    }
}
