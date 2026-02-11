using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Subjects.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Subjects.Handlers;

public class DeleteSubjectHandler : IRequestHandler<DeleteSubjectCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public DeleteSubjectHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (subject == null) return false;

        subject.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}