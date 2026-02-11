using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Students.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Students.Handlers;

public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IEduPortalDbContext _context;

    public DeleteStudentHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (student == null) return false;

        student.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}