using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class CreateMatchController : Controller
    {
        public IActionResult CreateMatch()
        {
            return View();
        }

        
    }
}
