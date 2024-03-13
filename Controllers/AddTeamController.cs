using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            return View(); //new AddTeamModel()
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(AddTeamModel team, IFormFile UploadImg)
        {
            try
            {
                if (UploadImg != null && UploadImg.Length > 0)
                {
                    // Handle file upload
                    string filename = Path.GetFileName(UploadImg.FileName);
                    string uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "TeamLogo");

                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    string uploadFilePath = Path.Combine(uploadFolderPath, filename);
                    using (var stream = new FileStream(uploadFilePath, FileMode.Create))
                    {
                        await UploadImg.CopyToAsync(stream);
                    }

                    team.TeamLogo = filename;

                    TeamDBHandler dBHandler = new TeamDBHandler();
                    if (dBHandler.AddTeam(team))
                    {
                        TempData["AlertMessage"] = "Team Added Successfully";
                        ModelState.Clear();
                        return RedirectToAction("AddTeam");
                    }
                }
                else
                {
                    TempData["AlertMessage"] = "Please select a file.";
                }
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Error: " + ex.Message;
            }

            return View(team);
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
