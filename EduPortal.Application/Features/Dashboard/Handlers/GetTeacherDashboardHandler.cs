using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Dashboard.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Dashboard.Handlers;

public class GetTeacherDashboardHandler : IRequestHandler<GetTeacherDashboardQuery, TeacherDashboardDto>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetTeacherDashboardHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TeacherDashboardDto> Handle(GetTeacherDashboardQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == request.TeacherId, cancellationToken);

        var myClasses = await _context.Classes
            .Where(c => c.TeacherId == request.TeacherId)
            .ProjectTo<ClassDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalStudents = await _context.Students.CountAsync(cancellationToken);
        var totalClasses = await _context.Classes.CountAsync(cancellationToken);

        var activeAssignmentCount = await _context.Assignments
            .CountAsync(a => a.TeacherId == request.TeacherId && a.DueDate >= DateTime.UtcNow, cancellationToken);

        var recentAssignments = await _context.Assignments
            .Where(a => a.TeacherId == request.TeacherId)
            .OrderByDescending(a => a.CreatedAt)
            .Take(5)
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new TeacherDashboardDto
        {
            TeacherName = teacher?.User.FullName ?? "",
            Classes = myClasses,
            TotalStudents = totalStudents,
            TotalClasses = totalClasses,
            ActiveAssignmentCount = activeAssignmentCount,
            RecentAssignments = recentAssignments,
            OverallSuccessRate = 0
        };
    }
}