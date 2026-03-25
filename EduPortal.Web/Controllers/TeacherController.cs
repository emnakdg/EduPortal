using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Assignments.Commands;
using EduPortal.Application.Features.Assignments.Queries;
using EduPortal.Application.Features.Classes.Queries;
using EduPortal.Application.Features.Dashboard.Queries;
using EduPortal.Application.Features.Students.Queries;
using EduPortal.Application.Features.Subjects.Queries;
using EduPortal.Application.Features.Teachers.Queries;
using EduPortal.Application.Features.TeacherNotes.Commands;
using EduPortal.Application.Features.TeacherNotes.Queries;
using EduPortal.Domain.Entities;
using EduPortal.Web.ViewModels.Teacher;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.Web.Controllers;

[Authorize(Roles = "Teacher")]
public class TeacherController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public TeacherController(IMediator mediator, UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    private async Task<int> GetTeacherIdAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return 0;
        var allTeachers = await _mediator.Send(new GetAllTeachersQuery());
        var teacher = allTeachers.FirstOrDefault(t => t.UserId == user.Id);
        return teacher?.Id ?? 0;
    }

    private async Task<TeacherDto?> GetCurrentTeacherAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return null;
        var allTeachers = await _mediator.Send(new GetAllTeachersQuery());
        return allTeachers.FirstOrDefault(t => t.UserId == user.Id);
    }

    // ─── Dashboard ───────────────────────────────────────────────────────────

    public async Task<IActionResult> Dashboard()
    {
        var teacherId = await GetTeacherIdAsync();
        var data = await _mediator.Send(new GetTeacherDashboardQuery(teacherId));
        return View(data);
    }

    // ─── Sınıflar ────────────────────────────────────────────────────────────

    public async Task<IActionResult> Classes()
    {
        var teacherId = await GetTeacherIdAsync();
        var classes = await _mediator.Send(new GetClassesByTeacherQuery(teacherId));
        return View(classes);
    }

    // ─── Ödevler ─────────────────────────────────────────────────────────────

    public async Task<IActionResult> Assignments(string? success)
    {
        var teacherId = await GetTeacherIdAsync();
        var assignments = await _mediator.Send(new GetAssignmentsByTeacherQuery(teacherId));
        ViewBag.SuccessMessage = success;
        return View(assignments);
    }

    public async Task<IActionResult> CreateAssignment()
    {
        await PopulateAssignmentViewBags();
        return View();
    }

    private async Task PopulateAssignmentViewBags()
    {
        var classes = await _mediator.Send(new GetAllClassesQuery());
        var allSubjects = await _mediator.Send(new GetAllSubjectsQuery());
        var students = await _mediator.Send(new GetAllStudentsQuery());

        var user = await _userManager.GetUserAsync(User);
        var allTeachers = await _mediator.Send(new GetAllTeachersQuery());
        var teacher = allTeachers.FirstOrDefault(t => t.UserId == user!.Id);
        var branch = teacher?.Branch ?? "";

        var filteredSubjects = allSubjects.Where(s => s.Name == branch).ToList();

        ViewBag.Classes = classes;
        ViewBag.Subjects = filteredSubjects;
        ViewBag.Students = students;
        ViewBag.TeacherBranch = branch;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAssignment(CreateAssignmentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateAssignmentViewBags();
            return View(model);
        }

        try
        {
            var teacherId = await GetTeacherIdAsync();
            await _mediator.Send(new CreateAssignmentCommand(
                model.Title,
                model.Description,
                model.QuestionCount,
                model.DueDate,
                model.Priority,
                teacherId,
                model.TopicId,
                model.StudentIds ?? new List<int>()
            ));
            return RedirectToAction("Assignments", new { success = "Ödev başarıyla oluşturuldu!" });
        }
        catch (FluentValidation.ValidationException vex)
        {
            foreach (var error in vex.Errors)
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            await PopulateAssignmentViewBags();
            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
            await PopulateAssignmentViewBags();
            return View(model);
        }
    }

    public async Task<IActionResult> EditAssignment(int id)
    {
        var teacherId = await GetTeacherIdAsync();
        var assignments = await _mediator.Send(new GetAssignmentsByTeacherQuery(teacherId));
        var a = assignments.FirstOrDefault(x => x.Id == id);
        if (a == null) return RedirectToAction("Assignments");

        var model = new EditAssignmentViewModel
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            QuestionCount = a.QuestionCount,
            DueDate = a.DueDate,
            Priority = a.Priority
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAssignment(EditAssignmentViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _mediator.Send(new UpdateAssignmentCommand(
            model.Id, model.Title, model.Description,
            model.QuestionCount, model.DueDate, model.Priority));

        if (result)
            return RedirectToAction("Assignments", new { success = "Ödev güncellendi!" });

        ModelState.AddModelError(string.Empty, "Ödev bulunamadı.");
        return View(model);
    }

    public async Task<IActionResult> AssignmentDetail(int id)
    {
        var detail = await _mediator.Send(new GetAssignmentDetailQuery(id));
        if (detail == null)
            return RedirectToAction("Assignments");
        return View(detail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GiveFeedback(int studentAssignmentId, string feedback, int assignmentId)
    {
        await _mediator.Send(new GiveFeedbackCommand(studentAssignmentId, feedback));
        TempData["success"] = "Geri bildirim kaydedildi!";
        return RedirectToAction("AssignmentDetail", new { id = assignmentId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        await _mediator.Send(new DeleteAssignmentCommand(id));
        return RedirectToAction("Assignments", new { success = "Ödev silindi." });
    }

    // ─── Notlar ──────────────────────────────────────────────────────────────

    public async Task<IActionResult> Notes(string? success)
    {
        var teacherId = await GetTeacherIdAsync();
        var notes = await _mediator.Send(new GetNotesByTeacherQuery(teacherId));
        var students = await _mediator.Send(new GetAllStudentsQuery());
        ViewBag.Students = students;
        ViewBag.SuccessMessage = success;
        return View(notes);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNote(string content, int studentId)
    {
        var teacherId = await GetTeacherIdAsync();
        await _mediator.Send(new CreateTeacherNoteCommand(content, teacherId, studentId));
        return RedirectToAction("Notes", new { success = "Not kaydedildi!" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteNote(int id)
    {
        await _mediator.Send(new DeleteTeacherNoteCommand(id));
        return RedirectToAction("Notes", new { success = "Not silindi." });
    }

    // ─── Profil ──────────────────────────────────────────────────────────────

    public async Task<IActionResult> Profile()
    {
        var teacher = await GetCurrentTeacherAsync();
        if (teacher == null) return RedirectToAction("Dashboard");

        var model = new TeacherProfileViewModel
        {
            FullName = teacher.FullName,
            Email = teacher.Email,
            Branch = teacher.Branch,
            ClassCount = teacher.ClassCount,
            StudentCount = teacher.StudentCount
        };
        return View(model);
    }

    public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
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