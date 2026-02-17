using Microsoft.AspNetCore.Mvc;

namespace EduPortal.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Error(string? message = null)
    {
        ViewBag.ErrorMessage = message;
        return View();
    }
}