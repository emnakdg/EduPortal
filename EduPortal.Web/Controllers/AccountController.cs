using EduPortal.Application.Features.Students.Commands;
using EduPortal.Domain.Entities;
using EduPortal.Web.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.Web.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMediator mediator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("RedirectByRole");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user!);

            if (roles.Contains("Admin"))
                return RedirectToAction("Dashboard", "Admin");
            else if (roles.Contains("Teacher"))
                return RedirectToAction("Dashboard", "Teacher");
            else
                return RedirectToAction("Dashboard", "Student");
        }

        ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("RedirectByRole");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _mediator.Send(new CreateStudentCommand(
                model.FullName, model.Email, model.Password, model.StudentNumber, null
            ));

            // Otomatik giriş yap
            await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            return RedirectToAction("Dashboard", "Student");
        }
        catch (FluentValidation.ValidationException vex)
        {
            foreach (var error in vex.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }
            return View(model);
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError(string.Empty, msg);
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    private IActionResult RedirectByRole()
    {
        if (User.IsInRole("Admin"))
            return RedirectToAction("Dashboard", "Admin");
        if (User.IsInRole("Teacher"))
            return RedirectToAction("Dashboard", "Teacher");
        return RedirectToAction("Dashboard", "Student");
    }
}