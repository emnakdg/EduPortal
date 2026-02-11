using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Students.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Students.Handlers;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public UpdateStudentHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (student == null) return false;

        student.User.FullName = request.FullName;
        student.StudentNumber = request.StudentNumber;
        student.ClassId = request.ClassId;
        student.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}