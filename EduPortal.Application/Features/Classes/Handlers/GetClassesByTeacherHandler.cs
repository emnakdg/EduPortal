using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Classes.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Classes.Handlers;

public class GetClassesByTeacherHandler : IRequestHandler<GetClassesByTeacherQuery, List<ClassDto>>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetClassesByTeacherHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClassDto>> Handle(GetClassesByTeacherQuery request, CancellationToken cancellationToken)
    {
        return await _context.Classes
            .Where(c => c.TeacherId == request.TeacherId)
            .ProjectTo<ClassDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
