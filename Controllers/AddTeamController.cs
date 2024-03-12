using CricketWebApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class AddTeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddTeam()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddTeam(AddTeamModel team)
        {
            if (ModelState.IsValid)
            {
                TeamDBHandler dbHandler = new TeamDBHandler();

                if (dbHandler.AddTeam(team))
                {
                    TempData["AlertMessage"] = "Team Added Successfully";

                    // Show alert when 11 players are added
                    if (team.TeamPlayerName.Length == 11)
                    {
                        TempData["AlertMessage"] = "Team Added Successfully. 11 players added.";
                    }

                    ModelState.Clear();
                    return RedirectToAction("AddTeam");
                }
                else
                {
                    TempData["AlertMessage"] = "Failed to add team. Please try again.";
                }
            }

            return View();
        }

        [HttpGet]
        public JsonResult GetPlayerSuggestions(string query)
        {
            TeamDBHandler dbHandler = new TeamDBHandler();
            List<string> playerSuggestions = dbHandler.GetPlayerSuggestions(query);
            return Json(playerSuggestions);
        }

    }
}
