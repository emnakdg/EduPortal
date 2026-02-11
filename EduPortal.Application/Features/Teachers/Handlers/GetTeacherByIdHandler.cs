using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Teachers.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Teachers.Handlers;

public class GetTeacherByIdHandler : IRequestHandler<GetTeacherByIdQuery, TeacherDto?>
{
    private readonly IEduPortalDbContext _context;
    private readonly IMapper _mapper;

    public GetTeacherByIdHandler(IEduPortalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TeacherDto?> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Teachers
            .Where(t => t.Id == request.Id)
            .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}