using CricketWebApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class CreateMatchController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateMatchController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /*  [HttpPost]
          public IActionResult CreateMatch()
          {
              return View();
          }*/

        public IActionResult CreateMatch()
        {
            CreateMatchDBHandler dbHandler = new CreateMatchDBHandler();
            List<AddTeamModel> teams = dbHandler.GetTeams();
            ViewBag.Teams = teams;
            return View();
        }

        // Example action using a repository pattern
      /*  public IActionResult GetTeamLogo(int teamId)
        {
            string logoPath = Path.Combine(_hostingEnvironment.WebRootPath, "TeamLogo");
            return PhysicalFile(logoPath, "image/jpeg"); // Or appropriate image format
        }*/

    }
}
