using EduPortal.Application.DTOs;
using EduPortal.Application.Features.Classes.Commands;
using EduPortal.Application.Features.Classes.Queries;
using EduPortal.Application.Features.Dashboard.Queries;
using EduPortal.Application.Features.Students.Commands;
using EduPortal.Application.Features.Students.Queries;
using EduPortal.Application.Features.Teachers.Commands;
using EduPortal.Application.Features.Teachers.Queries;
using EduPortal.Web.ViewModels.Admin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Dashboard()
    {
        var data = await _mediator.Send(new GetAdminDashboardQuery());
        return View(data);
    }


    public async Task<IActionResult> Teachers(string? success)
    {
        var teachers = await _mediator.Send(new GetAllTeachersQuery());
        ViewBag.SuccessMessage = success;
        return View(teachers);
    }

    public IActionResult CreateTeacher()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTeacher(CreateTeacherViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _mediator.Send(new CreateTeacherCommand(
                model.FullName, model.Email, model.Password, model.Branch
            ));
            return RedirectToAction("Teachers", new { success = "Öğretmen başarıyla eklendi!" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    public async Task<IActionResult> EditTeacher(int id)
    {
        var teacher = await _mediator.Send(new GetTeacherByIdQuery(id));
        if (teacher == null) return NotFound();

        var model = new EditTeacherViewModel
        {
            Id = teacher.Id,
            FullName = teacher.FullName,
            Email = teacher.Email,
            Branch = teacher.Branch
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTeacher(EditTeacherViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _mediator.Send(new UpdateTeacherCommand(model.Id, model.FullName, model.Branch));
        return RedirectToAction("Teachers", new { success = "Öğretmen bilgileri güncellendi!" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        await _mediator.Send(new DeleteTeacherCommand(id));
        return RedirectToAction("Teachers", new { success = "Öğretmen silindi." });
    }


    public async Task<IActionResult> Students(string? success)
    {
        var students = await _mediator.Send(new GetAllStudentsQuery());
        ViewBag.SuccessMessage = success;
        return View(students);
    }

    public async Task<IActionResult> CreateStudent()
    {
        var classes = await _mediator.Send(new GetAllClassesQuery());
        ViewBag.Classes = classes;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStudent(CreateStudentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Classes = await _mediator.Send(new GetAllClassesQuery());
            return View(model);
        }

        try
        {
            await _mediator.Send(new CreateStudentCommand(
                model.FullName, model.Email, model.Password, model.StudentNumber, model.ClassId
            ));
            return RedirectToAction("Students", new { success = "Öğrenci başarıyla eklendi!" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.Classes = await _mediator.Send(new GetAllClassesQuery());
            return View(model);
        }
    }
    public async Task<IActionResult> EditStudent(int id)
    {
        var student = await _mediator.Send(new GetStudentByIdQuery(id));
        if (student == null) return NotFound();

        ViewBag.Classes = await _mediator.Send(new GetAllClassesQuery());
        var model = new EditStudentViewModel
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            StudentNumber = student.StudentNumber,
            ClassId = null // ClassId dto'da yok, sınıf adından bulunacak
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditStudent(EditStudentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Classes = await _mediator.Send(new GetAllClassesQuery());
            return View(model);
        }

        await _mediator.Send(new UpdateStudentCommand(model.Id, model.FullName, model.StudentNumber, model.ClassId));
        return RedirectToAction("Students", new { success = "Öğrenci bilgileri güncellendi!" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        await _mediator.Send(new DeleteStudentCommand(id));
        return RedirectToAction("Students", new { success = "Öğrenci silindi." });
    }


    public async Task<IActionResult> Classes(string? success)
    {
        var classes = await _mediator.Send(new GetAllClassesQuery());
        ViewBag.SuccessMessage = success;
        return View(classes);
    }

    public async Task<IActionResult> CreateClass()
    {
        var teachers = await _mediator.Send(new GetAllTeachersQuery());
        ViewBag.Teachers = teachers;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateClass(CreateClassViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Teachers = await _mediator.Send(new GetAllTeachersQuery());
            return View(model);
        }

        try
        {
            await _mediator.Send(new CreateClassCommand(
                model.Name, model.Semester, model.Room, model.TeacherId
            ));
            return RedirectToAction("Classes", new { success = "Sınıf başarıyla oluşturuldu!" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.Teachers = await _mediator.Send(new GetAllTeachersQuery());
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteClass(int id)
    {
        await _mediator.Send(new DeleteClassCommand(id));
        return RedirectToAction("Classes", new { success = "Sınıf silindi." });
    }

    public async Task<IActionResult> EditClass(int id)
    {
        var cls = await _mediator.Send(new GetClassByIdQuery(id));
        if (cls == null) return NotFound();

        ViewBag.Teachers = await _mediator.Send(new GetAllTeachersQuery());
        var model = new EditClassViewModel
        {
            Id = cls.Id,
            Name = cls.Name,
            Semester = cls.Semester,
            Room = cls.Room,
            TeacherId = 0 // TeacherId dto'da yok
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditClass(EditClassViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Teachers = await _mediator.Send(new GetAllTeachersQuery());
            return View(model);
        }

        await _mediator.Send(new UpdateClassCommand(model.Id, model.Name, model.Semester, model.Room, model.TeacherId));
        return RedirectToAction("Classes", new { success = "Sınıf bilgileri güncellendi!" });
    }
}