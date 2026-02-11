using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Classes.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Classes.Handlers;

public class GetAllClassesHandler : IRequestHandler<GetAllClassesQuery, List<ClassDto>>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetAllClassesHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClassDto>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Classes
            .ProjectTo<ClassDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}