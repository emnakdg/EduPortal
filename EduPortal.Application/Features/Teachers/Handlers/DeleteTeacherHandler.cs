using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Teachers.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Teachers.Handlers;

public class DeleteTeacherHandler : IRequestHandler<DeleteTeacherCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public DeleteTeacherHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        if (teacher == null) return false;

        teacher.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}