using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Controllers
{
    public class AddRecordController : Controller
    {
        public IActionResult AddRecord()
        {
            return View();
        }
    }
}
