using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.TeacherNotes.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.TeacherNotes.Handlers;

public class GetNotesByTeacherHandler : IRequestHandler<GetNotesByTeacherQuery, List<TeacherNoteDto>>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetNotesByTeacherHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TeacherNoteDto>> Handle(GetNotesByTeacherQuery request, CancellationToken cancellationToken)
    {
        return await _context.TeacherNotes
            .Where(n => n.TeacherId == request.TeacherId)
            .OrderByDescending(n => n.CreatedAt)
            .ProjectTo<TeacherNoteDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
