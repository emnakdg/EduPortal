using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Assignments.Commands;
using EduPortal.Application.Features.Assignments.Queries;
using EduPortal.Application.Features.Classes.Queries;
using EduPortal.Application.Features.Dashboard.Queries;
using EduPortal.Application.Features.Students.Queries;
using EduPortal.Application.Features.Subjects.Queries;
using EduPortal.Application.Features.Teachers.Queries;
using EduPortal.Domain.Entities;
using EduPortal.Domain.Enums;
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

    public async Task<IActionResult> Dashboard()
    {
        var teacherId = await GetTeacherIdAsync();
        var data = await _mediator.Send(new GetTeacherDashboardQuery(teacherId));
        return View(data);
    }

    public async Task<IActionResult> Classes()
    {
        var classes = await _mediator.Send(new GetAllClassesQuery());
        return View(classes);
    }

    public async Task<IActionResult> Assignments()
    {
        var teacherId = await GetTeacherIdAsync();
        var assignments = await _mediator.Send(new GetAssignmentsByTeacherQuery(teacherId));
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
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }
            await PopulateAssignmentViewBags();
            return View(model);
        }
        catch (Exception ex)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError(string.Empty, errorMsg);
            await PopulateAssignmentViewBags();
            return View(model);
        }
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
}