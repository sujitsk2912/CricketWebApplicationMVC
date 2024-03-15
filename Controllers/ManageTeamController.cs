using CricketWebApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class ManageTeamController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ManageTeamController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult ManageTeam(int TeamID)
        {
            ManageTeamDBHandler dbHandler = new ManageTeamDBHandler();
            var Team = dbHandler.GetRecords().Find(get => get.TeamID == TeamID);
            return View(Team);
        }
    }
}
