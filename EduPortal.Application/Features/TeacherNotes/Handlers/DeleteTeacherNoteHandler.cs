using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.TeacherNotes.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.TeacherNotes.Handlers;

public class DeleteTeacherNoteHandler : IRequestHandler<DeleteTeacherNoteCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public DeleteTeacherNoteHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTeacherNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _context.TeacherNotes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (note == null) return false;

        _context.TeacherNotes.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
