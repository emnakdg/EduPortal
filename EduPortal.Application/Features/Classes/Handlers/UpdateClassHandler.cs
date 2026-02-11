using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Classes.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Classes.Handlers;

public class UpdateClassHandler : IRequestHandler<UpdateClassCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public UpdateClassHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Classes
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (entity == null) return false;

        entity.Name = request.Name;
        entity.Semester = request.Semester;
        entity.Room = request.Room;
        entity.TeacherId = request.TeacherId;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}