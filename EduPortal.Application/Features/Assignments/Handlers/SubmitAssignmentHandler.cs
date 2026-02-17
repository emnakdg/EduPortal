using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Assignments.Commands;
using EduPortal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Assignments.Handlers;

public class SubmitAssignmentHandler : IRequestHandler<SubmitAssignmentCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public SubmitAssignmentHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(SubmitAssignmentCommand request, CancellationToken cancellationToken)
    {
        var studentAssignment = await _context.StudentAssignments
            .FirstOrDefaultAsync(sa => sa.Id == request.StudentAssignmentId, cancellationToken);

        if (studentAssignment == null) return false;

        studentAssignment.CorrectAnswers = request.CorrectAnswers;
        studentAssignment.WrongAnswers = request.WrongAnswers;
        studentAssignment.EmptyAnswers = request.EmptyAnswers;
        studentAssignment.Status = AssignmentStatus.Completed;
        studentAssignment.CompletedAt = DateTime.UtcNow;

        // Score: doğru cevap yüzdesi
        var totalAnswered = request.CorrectAnswers + request.WrongAnswers;
        studentAssignment.Score = totalAnswered > 0
            ? (int)Math.Round((double)request.CorrectAnswers / totalAnswered * 100)
            : 0;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
