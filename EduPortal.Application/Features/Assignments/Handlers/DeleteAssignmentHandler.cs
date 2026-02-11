using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Assignments.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Assignments.Handlers;

public class DeleteAssignmentHandler : IRequestHandler<DeleteAssignmentCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public DeleteAssignmentHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (assignment == null) return false;

        assignment.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}