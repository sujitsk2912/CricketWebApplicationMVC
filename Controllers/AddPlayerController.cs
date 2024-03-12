﻿using Microsoft.AspNetCore.Hosting;
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
        public IActionResult AddPlayer(AddPlayerModel PList)
        {
            if (ModelState.IsValid)
            {
                PlayerDBHandler dBHandler = new PlayerDBHandler();

                // Check if a file is uploaded
                var file = Request.Form.Files["PlayerImg"];
                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "PlayerImg");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    PList.PlayerImg = filePath;
                }

                if (dBHandler.InsertRecord(PList))
                {
                    TempData["AlertMessage"] = "Player Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("AddPlayer");
                }
            }

            return View(PList);
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