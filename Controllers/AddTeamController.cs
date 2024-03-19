using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
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

        [HttpGet]
        public IActionResult ManageTeam(int TeamID)
        {
            ManageTeamDBHandler dbHandler = new ManageTeamDBHandler();
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

                ManageTeamDBHandler dBHandler = new ManageTeamDBHandler();

                dBHandler.GetRecords().Find(getDetails => getDetails.TeamID == TeamID);

                if (dBHandler.UpdateRecord(iList))
                {
                    TempData["AlertMessage"] = "Record Edited Successfully";
                    ModelState.Clear();
                    return RedirectToAction("ManageTeam");
                }
                else
                {
                    TempData["AlertMessage"] = "Incorrect Team ID";
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View(iList);
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
                /*  if (ModelState.IsValid)
                  {
                      bool teamExists = CheckTeamExists(team.TeamName);

                      if (teamExists)
                      {
                          TempData["AlertMessage"] = "Team with the same name already exists.";
                          return RedirectToAction("AddTeam");
                      }
                      else
                      {*/
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

       /* private bool CheckTeamExists(string teamName)
        {
            string connection = "Data Source = LAPTOP-CNVSH31R\\SQLEXPRESS01; Integrated Security=True; Database = Cricket_App";
            SqlConnection con = new SqlConnection(connection);
          
            string query = "SELECT COUNT(*) FROM AddTeams WHERE TeamName = @TeamName";

            int count;

            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@TeamName", teamName);
                count = (int)command.ExecuteScalar();
            }

            return count > 0;
        }*/

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
