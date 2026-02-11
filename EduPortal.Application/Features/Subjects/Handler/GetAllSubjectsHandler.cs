using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Subjects.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Subjects.Handlers;

public class GetAllSubjectsHandler : IRequestHandler<GetAllSubjectsQuery, List<SubjectDto>>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetAllSubjectsHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SubjectDto>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Subjects
            .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}