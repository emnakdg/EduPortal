using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Assignments.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Assignments.Handlers;

public class GetStudentAssignmentsHandler : IRequestHandler<GetStudentAssignmentsQuery, List<StudentAssignmentDto>>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentAssignmentsHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<StudentAssignmentDto>> Handle(GetStudentAssignmentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.StudentAssignments
            .Where(sa => sa.StudentId == request.StudentId)
            .OrderByDescending(sa => sa.Assignment.DueDate)
            .ProjectTo<StudentAssignmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
