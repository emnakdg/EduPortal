using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Assignments.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Assignments.Handlers;

public class GiveFeedbackHandler : IRequestHandler<GiveFeedbackCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public GiveFeedbackHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(GiveFeedbackCommand request, CancellationToken cancellationToken)
    {
        var studentAssignment = await _context.StudentAssignments
            .FirstOrDefaultAsync(sa => sa.Id == request.StudentAssignmentId, cancellationToken);

        if (studentAssignment == null) return false;

        studentAssignment.TeacherFeedback = request.Feedback;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}