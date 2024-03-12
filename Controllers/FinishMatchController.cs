using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class FinishMatchController : Controller
    {
        public IActionResult FinishMatch()
        {
            return View();
        }
    }
}
