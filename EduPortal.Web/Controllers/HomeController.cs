using Microsoft.AspNetCore.Mvc;

namespace EduPortal.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            if (User.IsInRole("Admin")) return RedirectToAction("Dashboard", "Admin");
            if (User.IsInRole("Teacher")) return RedirectToAction("Dashboard", "Teacher");
            return RedirectToAction("Dashboard", "Student");
        }
        return View();
    }

    public IActionResult Error(string? message = null)
    {
        ViewBag.ErrorMessage = message;
        return View();
    }
}