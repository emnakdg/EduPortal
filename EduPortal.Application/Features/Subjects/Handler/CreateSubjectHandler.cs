using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Subjects.Commands;
using EduPortal.Domain.Entities;
using MediatR;

namespace EduPortal.Application.Features.Subjects.Handlers;

public class CreateSubjectHandler : IRequestHandler<CreateSubjectCommand, int>
{
    private readonly IEduPortalDbContext _context;

    public CreateSubjectHandler(IEduPortalDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = new Subject
        {
            Name = request.Name,
            Code = request.Code,
            Color = request.Color,
            GradeLevel = request.GradeLevel
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync(cancellationToken);
        return subject.Id;
    }
}