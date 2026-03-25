using EduPortal.Application.Features.Assignments.Commands;
using EduPortal.Application.Features.Assignments.Queries;
using EduPortal.Application.Features.Dashboard.Queries;
using EduPortal.Application.Features.Students.Queries;
using EduPortal.Domain.Entities;
using EduPortal.Web.ViewModels.Student;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.Web.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public StudentController(IMediator mediator, UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    private async Task<int> GetStudentIdAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return 0;
        var allStudents = await _mediator.Send(new GetAllStudentsQuery());
        var student = allStudents.FirstOrDefault(s => s.Email == user.Email);
        return student?.Id ?? 0;
    }

    // GET: /Student/Dashboard
    public async Task<IActionResult> Dashboard()
    {
        var studentId = await GetStudentIdAsync();
        var dashboard = await _mediator.Send(new GetStudentDashboardQuery(studentId));
        return View(dashboard);
    }

    // GET: /Student/Assignments
    public async Task<IActionResult> Assignments()
    {
        var studentId = await GetStudentIdAsync();
        var assignments = await _mediator.Send(new GetStudentAssignmentsQuery(studentId));
        return View(assignments);
    }

    // GET: /Student/AssignmentDetail/5
    public async Task<IActionResult> AssignmentDetail(int id)
    {
        var studentId = await GetStudentIdAsync();
        var assignments = await _mediator.Send(new GetStudentAssignmentsQuery(studentId));
        var assignment = assignments.FirstOrDefault(a => a.Id == id);

        if (assignment == null)
            return RedirectToAction("Assignments");

        return View(assignment);
    }

    // POST: /Student/SubmitAssignment
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitAssignment(int studentAssignmentId, int correctAnswers, int wrongAnswers, int emptyAnswers)
    {
        var result = await _mediator.Send(new SubmitAssignmentCommand(
            studentAssignmentId,
            correctAnswers,
            wrongAnswers,
            emptyAnswers
        ));

        if (result)
            TempData["success"] = "Ödev başarıyla teslim edildi!";
        else
            TempData["error"] = "Ödev bulunamadı.";

        return RedirectToAction("Assignments");
    }

    // ─── Profil ──────────────────────────────────────────────────────────────

    public async Task<IActionResult> Profile()
    {
        var studentId = await GetStudentIdAsync();
        var allStudents = await _mediator.Send(new GetAllStudentsQuery());
        var student = allStudents.FirstOrDefault(s => s.Id == studentId);
        if (student == null) return RedirectToAction("Dashboard");

        var model = new StudentProfileViewModel
        {
            FullName = student.FullName,
            Email = student.Email,
            StudentNumber = student.StudentNumber,
            ClassName = student.ClassName,
            CompletedAssignments = student.CompletedAssignments,
            PendingAssignments = student.PendingAssignments,
            SuccessRate = student.SuccessRate
        };
        return View(model);
    }

    public IActionResult ChangePassword() => View(new StudentChangePasswordViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(StudentChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Dashboard");

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (result.Succeeded)
        {
            TempData["success"] = "Şifreniz başarıyla değiştirildi!";
            return RedirectToAction("Profile");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }
}