using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.Features.Teachers.Commands;
using EduPortal.Domain.Entities;
using EduPortal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduPortal.Application.Features.Teachers.Handlers;

public class CreateTeacherHandler : IRequestHandler<CreateTeacherCommand, int>
{
    private readonly IEduPortalDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public CreateTeacherHandler(IEduPortalDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<int> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            Role = UserRole.Teacher,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Teacher");

        var teacher = new Teacher
        {
            UserId = user.Id,
            Branch = request.Branch
        };

        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync(cancellationToken);

        return teacher.Id;
    }
}