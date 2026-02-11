using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Dashboard.Queries;
using EduPortal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Dashboard.Handlers;

public class GetStudentDashboardHandler : IRequestHandler<GetStudentDashboardQuery, StudentDashboardDto>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentDashboardHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<StudentDashboardDto> Handle(GetStudentDashboardQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

        var studentAssignments = await _context.StudentAssignments
            .Include(sa => sa.Assignment)
                .ThenInclude(a => a.Topic).ThenInclude(t => t.Unit).ThenInclude(u => u.Subject)
            .Include(sa => sa.Assignment).ThenInclude(a => a.Teacher).ThenInclude(t => t.User)
            .Where(sa => sa.StudentId == request.StudentId)
            .ToListAsync(cancellationToken);

        var activeAssignments = studentAssignments
            .Where(sa => sa.Status == AssignmentStatus.Pending || sa.Status == AssignmentStatus.InProgress)
            .Select(sa => _mapper.Map<AssignmentDto>(sa.Assignment))
            .ToList();

        return new StudentDashboardDto
        {
            StudentName = student?.User.FullName ?? "",
            PendingCount = studentAssignments.Count(sa => sa.Status == AssignmentStatus.Pending),
            CompletedCount = studentAssignments.Count(sa => sa.Status == AssignmentStatus.Completed),
            TotalQuestionsSolved = studentAssignments
                .Where(sa => sa.Status == AssignmentStatus.Completed)
                .Sum(sa => sa.CorrectAnswers + sa.WrongAnswers),
            Streak = 15,
            ActiveAssignments = activeAssignments
        };
    }
}