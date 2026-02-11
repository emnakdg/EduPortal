using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Subjects.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Subjects.Handlers;

public class GetSubjectByIdHandler : IRequestHandler<GetSubjectByIdQuery, SubjectDto?>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetSubjectByIdHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SubjectDto?> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Subjects
            .Where(s => s.Id == request.Id)
            .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}