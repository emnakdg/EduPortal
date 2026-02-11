using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Assignments.Commands;
using EduPortal.Domain.Entities;
using EduPortal.Domain.Enums;
using MediatR;

namespace EduPortal.Application.Features.Assignments.Handlers;

public class CreateAssignmentHandler : IRequestHandler<CreateAssignmentCommand, int>
{
    private readonly IEduPortalDbContext _context;

    public CreateAssignmentHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = new Assignment
        {
            Title = request.Title,
            Description = request.Description,
            QuestionCount = request.QuestionCount,
            DueDate = request.DueDate,
            Priority = request.Priority,
            TeacherId = request.TeacherId,
            TopicId = request.TopicId
        };

        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var studentId in request.StudentIds)
        {
            _context.StudentAssignments.Add(new StudentAssignment
            {
                StudentId = studentId,
                AssignmentId = assignment.Id,
                Status = AssignmentStatus.Pending
            });
        }

        await _context.SaveChangesAsync(cancellationToken);
        return assignment.Id;
    }
}