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

                var getdata = dBHandler.GetRecords().Find(getDetails => getDetails.TeamID == TeamID);

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
                return View(getdata);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View(iList);
        }
    }
}
