using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Classes.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Classes.Handlers;

public class DeleteClassHandler : IRequestHandler<DeleteClassCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public DeleteClassHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Classes.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (entity == null) return false;

        entity.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}