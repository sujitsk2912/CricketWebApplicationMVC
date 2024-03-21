﻿using Microsoft.AspNetCore.Hosting;
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
            TeamDBHandler dBHandler = new TeamDBHandler();
            return View(dBHandler.GetRecords());
        }

        public IActionResult AddTeam()
        {
            return View();
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
                    else
                    {
                        TempData["AlertMessage"] = "Team Already Added";
                        ModelState.Clear();
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
        public IActionResult ManageTeam(int TeamID)
        {
            TeamDBHandler dbHandler = new TeamDBHandler();
            var Team = dbHandler.GetRecords().Find(get => get.TeamID == TeamID);
            return View(Team);
        }

        [HttpPost]
        public async Task<IActionResult> ManageTeam(int TeamID, AddTeamModel iList, IFormFile UploadFile)
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

                    iList.TeamLogo = filename;
                }

                TeamDBHandler dBHandler = new TeamDBHandler();

                if (dBHandler.UpdateRecord(iList))
                {
                    TempData["AlertMessage"] = "Record Edited Successfully";
                    ModelState.Clear();
                    return RedirectToAction("ManageTeam");
                }
                else
                {
                    TempData["AlertMessage"] = "This Team Already Exist";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View(iList);
        }


        [HttpGet]
        public JsonResult GetPlayerSuggestions(string query)
        {
            TeamDBHandler dbHandler = new TeamDBHandler();
            List<string> playerSuggestions = dbHandler.GetPlayerSuggestions(query);
            return Json(playerSuggestions);
        }

        public IActionResult Details(int TeamId)
        {
            TeamDBHandler dBHandler = new TeamDBHandler();
            return View(dBHandler.GetRecords().Find(getDetails => getDetails.TeamID == TeamId));
        }

        public IActionResult PlayerDetails(string PlayerName)
        {
            PlayerDBHandler dBHandler = new PlayerDBHandler();
            return View(dBHandler.GetRecords().Find(getDetails => getDetails.PlayerName == PlayerName));
        }

        public IActionResult Delete(int TeamID)
        {
            TeamDBHandler dBHandler = new TeamDBHandler();
            dBHandler.DeleteRecord(TeamID);
            return RedirectToAction("Index");
        }
    }
}
