using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using CricketWebApplicationMVC.Models;


namespace CricketWebApplicationMVC.Controllers
{
    public class AddTeamController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AddTeamController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

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
        public async Task<IActionResult> AddTeam(AddTeamModel team, IFormFile UploadFile)
        {
            try
            {
                if (UploadFile != null && UploadFile.Length > 0)
                {
                    string filename = Path.GetFileName(UploadFile.FileName);
                    string uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "TeamLogo");

                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    string uploadFilePath = Path.Combine(uploadFolderPath, filename);
                    using (var stream = new FileStream(uploadFilePath, FileMode.Create))
                    {
                        await UploadFile.CopyToAsync(stream);
                    }

                    team.TeamLogo = filename;

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
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Error: " + ex.Message;
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
