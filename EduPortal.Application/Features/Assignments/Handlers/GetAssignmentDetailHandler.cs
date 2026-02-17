using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Assignments.Queries;
using EduPortal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Assignments.Handlers;

public class GetAssignmentDetailHandler : IRequestHandler<GetAssignmentDetailQuery, AssignmentDetailDto?>
{
    private readonly IEduPortalDbContext _context;

    public GetAssignmentDetailHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<AssignmentDetailDto?> Handle(GetAssignmentDetailQuery request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Topic)
                .ThenInclude(t => t.Unit)
                    .ThenInclude(u => u.Subject)
            .FirstOrDefaultAsync(a => a.Id == request.AssignmentId, cancellationToken);

        if (assignment == null) return null;

        var studentAssignments = await _context.StudentAssignments
            .Include(sa => sa.Student)
                .ThenInclude(s => s.User)
            .Where(sa => sa.AssignmentId == request.AssignmentId)
            .ToListAsync(cancellationToken);

        var studentResults = studentAssignments.Select(sa => new StudentResultDto
        {
            StudentAssignmentId = sa.Id,
            StudentName = sa.Student.User.FullName,
            StudentNumber = sa.Student.StudentNumber,
            Status = sa.Status,
            CorrectAnswers = sa.CorrectAnswers,
            WrongAnswers = sa.WrongAnswers,
            EmptyAnswers = sa.EmptyAnswers,
            Score = sa.Score,
            CompletedAt = sa.CompletedAt,
            TeacherFeedback = sa.TeacherFeedback
        }).ToList();

        var completedResults = studentResults.Where(r => r.Status == AssignmentStatus.Completed).ToList();

        return new AssignmentDetailDto
        {
            Id = assignment.Id,
            Title = assignment.Title,
            Description = assignment.Description,
            TopicName = assignment.Topic.Name,
            SubjectName = assignment.Topic.Unit.Subject.Name,
            QuestionCount = assignment.QuestionCount,
            DueDate = assignment.DueDate,
            Priority = assignment.Priority,
            TotalStudents = studentResults.Count,
            CompletedStudents = completedResults.Count,
            AverageScore = completedResults.Any()
                ? completedResults.Where(r => r.Score.HasValue).Average(r => r.Score!.Value)
                : 0,
            StudentResults = studentResults
        };
    }
}