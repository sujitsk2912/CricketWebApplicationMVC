using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class ManageTeamController : Controller
    {
        public IActionResult ManageTeam()
        {
            return View();
        }
    }
}
