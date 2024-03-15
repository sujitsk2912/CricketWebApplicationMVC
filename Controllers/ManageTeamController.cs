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
      

        /* [HttpGet]
         public IActionResult ManageTeam(int TeamId)
         {
             ManageTeamDBHandler dBHandler = new ManageTeamDBHandler();
             return View(dBHandler.GetRecords().Find(getDetails => getDetails.TeamID == TeamId));
         }*/


        /* public IActionResult ManageTeam(int TeamID)
         {
             // Check if TeamID is provided
             if (TeamID.HasValue)
             {
                 ManageTeamDBHandler dbHandler = new ManageTeamDBHandler();
                 AddTeamModel teamModel = dbHandler.GetRecords(TeamID.Value);

                 // Check if teamModel is not null
                 if (teamModel != null)
                 {
                     // Pass the retrieved data to the view
                     return View(teamModel);
                 }
                 else
                 {
                     // Handle the case where no records are found for the provided TeamID
                     TempData["AlertMessage"] = "No team found for the provided ID.";
                     return View(new ManageTeamModel()); // Assuming ManageTeamModel is the model for your view
                 }
             }
             else
             {
                 // Handle the case where TeamID is not provided
                 return View(new ManageTeamModel()); // Assuming ManageTeamModel is the model for your view
             }
         }*/

    }
}
