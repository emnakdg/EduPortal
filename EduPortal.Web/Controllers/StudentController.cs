using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.Web.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}