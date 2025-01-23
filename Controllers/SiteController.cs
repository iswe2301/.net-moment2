using Microsoft.AspNetCore.Mvc;

namespace Moment2_MVC.Controllers
{
    // Skapa en ny kontroller
    public class SiteController : Controller
    {
        // Skapa en ny action
        public IActionResult Start()
        {
            return View(); // Returnera vy
        }
    }
}