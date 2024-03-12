using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class ChangeStatusController : Controller
    {
        public IActionResult ChangeStatus()
        {
            return View();
        }
    }
}
