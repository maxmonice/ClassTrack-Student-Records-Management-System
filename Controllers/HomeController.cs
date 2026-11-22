using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClassTrack.Models;

namespace ClassTrack.Controllers;

// Only hosts the shared error page (see UseExceptionHandler in Program.cs).
public class HomeController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
