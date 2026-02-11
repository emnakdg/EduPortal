using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Students.Commands;
using EduPortal.Domain.Entities;
using EduPortal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduPortal.Application.Features.Students.Handlers;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IEduPortalDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public CreateStudentHandler(IEduPortalDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            Role = UserRole.Student,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Student");

        var student = new Student
        {
            UserId = user.Id,
            StudentNumber = request.StudentNumber,
            ClassId = request.ClassId
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);

        return student.Id;
    }
}