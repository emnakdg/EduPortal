using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Assignments.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Assignments.Handlers;

public class GetAssignmentsByTeacherHandler : IRequestHandler<GetAssignmentsByTeacherQuery, List<AssignmentDto>>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetAssignmentsByTeacherHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AssignmentDto>> Handle(GetAssignmentsByTeacherQuery request, CancellationToken cancellationToken)
    {
        return await _context.Assignments
            .Where(a => a.TeacherId == request.TeacherId)
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}