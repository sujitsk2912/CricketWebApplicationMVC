using CricketWebApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CricketWebApplicationMVC.Services;

namespace CricketWebApplicationMVC.Controllers
{
    public class CreateMatchController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateMatchController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult CreateMatch(int TeamID)
        {
            TeamDBHandler dbHandler = new TeamDBHandler();
            var teams = dbHandler.GetRecords();
            ViewBag.Teams = teams;
            var selectedTeam = teams.FirstOrDefault(team => team.TeamID == TeamID);
            return View(selectedTeam); 
        }

        [HttpPost]
        public IActionResult CreateMatch(CreateMatchModel match)
        {
            CreateMatchDBHandler dbHandler = new CreateMatchDBHandler();
            if (dbHandler.Create(match))
            {
                TempData["AlertMessage"] = "Match Created Successfully";
                ModelState.Clear();
                return RedirectToAction("CreateMatch");
            }
            else
            {
                TempData["AlertMessage"] = "Match creating failed";
                ModelState.Clear();
            }
            return View(match);
        }
    }
}
