using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Assignments.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Assignments.Handlers;

public class UpdateAssignmentHandler : IRequestHandler<UpdateAssignmentCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public UpdateAssignmentHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (assignment == null) return false;

        assignment.Title = request.Title;
        assignment.Description = request.Description;
        assignment.QuestionCount = request.QuestionCount;
        assignment.DueDate = request.DueDate;
        assignment.Priority = request.Priority;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
