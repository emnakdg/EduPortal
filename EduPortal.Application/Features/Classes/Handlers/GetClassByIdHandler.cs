using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Classes.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Classes.Handlers;

public class GetClassByIdHandler : IRequestHandler<GetClassByIdQuery, ClassDto?>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetClassByIdHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClassDto?> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Classes
            .Where(c => c.Id == request.Id)
            .ProjectTo<ClassDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}