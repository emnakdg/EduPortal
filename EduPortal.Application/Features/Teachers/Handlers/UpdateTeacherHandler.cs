using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Teachers.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Teachers.Handlers;

public class UpdateTeacherHandler : IRequestHandler<UpdateTeacherCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public UpdateTeacherHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (teacher == null) return false;

        teacher.User.FullName = request.FullName;
        teacher.Branch = request.Branch;
        teacher.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}