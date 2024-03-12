using Microsoft.AspNetCore.Mvc;
using CricketWebApplicationMVC.Models;
using Microsoft.Extensions.FileProviders;

namespace CricketWebApplicationMVC.Controllers
{
    public class AddPlayerController : Controller
    {
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
        public IActionResult AddPlayer(AddPlayerModel PList, IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                if (ImageFile.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(ImageFile.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    // Combines two strings into a path.
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PlayerImg")).Root + $@"\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        ImageFile.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }

            if (ModelState.IsValid)
            {
                PlayerDBHandler dBHandler = new PlayerDBHandler();

                if (dBHandler.InsertRecord(PList))
                {
                    TempData["AlertMessage"] = "Player Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("AddPlayer");
                }
            }

            return View();
        }

        public IActionResult Edit(int PlayerId)
        {
            PlayerDBHandler dbHandler = new PlayerDBHandler();
            return View(dbHandler.GetRecords().Find(get => get.PlayerID == PlayerId));
        }

        [HttpPost]
        public IActionResult Edit(int PlayerId, AddPlayerModel iList)
        {
            PlayerDBHandler dBHandler = new PlayerDBHandler();
            dBHandler.UpdateRecord(iList);
            return RedirectToAction("Index");
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
