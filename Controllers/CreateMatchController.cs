using CricketWebApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CreateMatch()
        {
            return View();
        }
    }
}
