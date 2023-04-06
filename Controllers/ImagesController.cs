using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PhotoShoot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using PhotoShoot.Models.Images;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoShoot.Data.Models;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;

namespace PhotoShoot.Controllers
{
	public class ImagesController : Controller
	{
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _dbContext;

        public ImagesController(IWebHostEnvironment hostEnvironment, ApplicationDbContext dbContext)
		{
            _hostEnvironment = hostEnvironment;
            _dbContext = dbContext;
        }

        //public IActionResult CreateImage()
        //{
        //    ViewBag.Categories = _dbContext.ImageCategories.ToList();
        //    ViewBag.Images = _dbContext.Images.Include(i => i.ImageCategory).ToList(); ;
        //    return View();

        //}

        public IActionResult CreateImage()
        {
            //var model = new ImageFormModel { Categories = categories };
            //return View(model);
            //var model = new ImageFormModel
            //{
            //    Categories = _dbContext.ImageCategories
            //        .Select(c => new ImageCategoryViewModel { ImageCategoryId = c.Id, Name = c.Name })
            //        .ToList()
            //};

            //return View(model);
            return View(new ImageFormModel
            {
                Categories = GetImageCategories()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImageAsync(ImageFormModel model)
        {
            //foreach (var key in ModelState.Keys)
            //{
            //    if (ModelState[key].Errors.Count > 0)
            //    {
            //        Console.WriteLine($"Key: {key} - Error: {ModelState[key].Errors[0].ErrorMessage}");
            //    }
            //}

            if (!ModelState.IsValid)
            {
                model.Categories = GetImageCategories();
                return View(model);
            }

            // Validate the relevant properties
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Description) || model.ImageFile == null || model.ImageCategoryId == 0)
            {
                model.Categories = await _dbContext.ImageCategories
                    .Select(c => new ImageCategoryViewModel { ImageCategoryId = c.Id, Name = c.Name })
                    .ToListAsync();

                return View(model);
            }

            // Save the uploaded file
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "assets/images/tara");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            // Save the image record to the database
            var image = new Image
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = "/assets/images/tara/" + uniqueFileName,
                ImageCategoryId = model.ImageCategoryId,    
            };

                _dbContext.Images.Add(image);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("AdminGallery", "Images");
            
        }
       
        public IActionResult AdminGallery()
        {
            var images = _dbContext.Images.Include(i => i.ImageCategory).ToList();
            ViewBag.Images = images;
            return View();
        }

        public IEnumerable<ImageCategoryViewModel> GetImageCategories()
            => this._dbContext
                .ImageCategories
                .Select(x => new ImageCategoryViewModel
                {
                    ImageCategoryId = x.Id,
                    Name = x.Name
                })
                .ToList();
    }
}

