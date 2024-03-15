using CricketWebApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class CreateMatchController : Controller
    {
        public IActionResult CreateMatch()
        {
            CreateMatchDBHandler dbHandler = new CreateMatchDBHandler();
            var teams = dbHandler.GetTeams(); // Fetch teams from the database
            return View(teams);
        }
    }
}
