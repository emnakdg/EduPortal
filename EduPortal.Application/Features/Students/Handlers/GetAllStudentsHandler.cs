using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Students.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Students.Handlers;

public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetAllStudentsHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Students
            .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}