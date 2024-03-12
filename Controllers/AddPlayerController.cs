using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using CricketWebApplicationMVC.Models;

namespace CricketWebApplicationMVC.Controllers
{
    public class AddPlayerController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AddPlayerController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            PlayerDBHandler dBHandler = new PlayerDBHandler();
            return View(dBHandler.GetRecords());
        }

        [HttpGet]
        public IActionResult AddPlayer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(AddPlayerModel PList, IFormFile UploadFile)
        {
            try
            {
                if (UploadFile != null && UploadFile.Length > 0)
                {
                    string filename = Path.GetFileName(UploadFile.FileName);
                    string uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "PlayerImg");

                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    string uploadFilePath = Path.Combine(uploadFolderPath, filename);
                    using (var stream = new FileStream(uploadFilePath, FileMode.Create))
                    {
                        await UploadFile.CopyToAsync(stream);
                    }

                    PList.PlayerImg = filename;

                    PlayerDBHandler dBHandler = new PlayerDBHandler();
                    if (dBHandler.InsertRecord(PList))
                    {
                        TempData["AlertMessage"] = "Player Added Successfully";
                        ModelState.Clear();
                        return RedirectToAction("AddPlayer");
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Player Already Added";
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

            return View(PList);
        }

        [HttpGet]
        public IActionResult Edit(int PlayerId)
        {
            PlayerDBHandler dbHandler = new PlayerDBHandler();
            var player = dbHandler.GetRecords().Find(get => get.PlayerID == PlayerId);
            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int PlayerId, AddPlayerModel iList, IFormFile UploadFile)
        {
            try
            {
                if (UploadFile != null && UploadFile.Length > 0)
                {
                    string filename = Path.GetFileName(UploadFile.FileName);
                    string uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "PlayerImg");

                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    string uploadFilePath = Path.Combine(uploadFolderPath, filename);
                    using (var stream = new FileStream(uploadFilePath, FileMode.Create))
                    {
                        await UploadFile.CopyToAsync(stream);
                    }

                    iList.PlayerImg = filename;
                }

                PlayerDBHandler dBHandler = new PlayerDBHandler();
                dBHandler.UpdateRecord(iList);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View(iList);
        }

        public IActionResult Details(int PlayerId)
        {
            PlayerDBHandler dBHandler = new PlayerDBHandler();
            return View(dBHandler.GetRecords().Find(getDetails => getDetails.PlayerID == PlayerId));
        }

        public IActionResult Delete(int PlayerId)
        {
            PlayerDBHandler dBHandler = new PlayerDBHandler();
            dBHandler.DeleteRecord(PlayerId);
            return RedirectToAction("Index");
        }

    }
}
