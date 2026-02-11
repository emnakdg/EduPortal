using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Dashboard.Queries;
using EduPortal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Dashboard.Handlers;

public class GetAdminDashboardHandler : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDto>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetAdminDashboardHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AdminDashboardDto> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
    {
        var recentStudents = await _context.Students
            .OrderByDescending(s => s.CreatedAt)
            .Take(5)
            .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new AdminDashboardDto
        {
            TotalStudents = await _context.Students.CountAsync(cancellationToken),
            TotalTeachers = await _context.Teachers.CountAsync(cancellationToken),
            TotalClasses = await _context.Classes.CountAsync(cancellationToken),
            PendingAssignments = await _context.StudentAssignments
                .CountAsync(sa => sa.Status == AssignmentStatus.Pending, cancellationToken),
            RecentStudents = recentStudents
        };
    }
}