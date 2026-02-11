using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Classes.Commands;
using EduPortal.Domain.Entities;
using MediatR;

namespace EduPortal.Application.Features.Classes.Handlers;

public class CreateClassHandler : IRequestHandler<CreateClassCommand, int>
{
    private readonly IEduPortalDbContext _context;

    public CreateClassHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var entity = new Class
        {
            Name = request.Name,
            Semester = request.Semester,
            Room = request.Room,
            TeacherId = request.TeacherId
        };

        _context.Classes.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}