using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Showyfx.Web.Entry.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Description = "让 .NET 开发更简单，更通用，更流行。";

        return View();
    }
}