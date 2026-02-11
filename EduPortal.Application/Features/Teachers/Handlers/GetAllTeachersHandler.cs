using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Teachers.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Teachers.Handlers;

public class GetAllTeachersHandler : IRequestHandler<GetAllTeachersQuery, List<TeacherDto>>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetAllTeachersHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TeacherDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Teachers
            .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}