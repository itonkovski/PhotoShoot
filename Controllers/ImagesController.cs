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

        public IActionResult CreateImage()
        {
            ViewBag.Categories = _dbContext.ImageCategories.ToList();
            ViewBag.Images = _dbContext.Images.Include(i => i.ImageCategory).ToList(); ;
            return View();

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateImage(ImageFormModel model)
        //{
        //    foreach (var key in ModelState.Keys)
        //    {
        //        if (ModelState[key].Errors.Count > 0)
        //        {
        //            Console.WriteLine($"Key: {key} - Error: {ModelState[key].Errors[0].ErrorMessage}");
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        string uniqueFileName = null;

        //        if (model.ImageFile != null)
        //        {
        //            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "assets/images/tara");
        //            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
        //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await model.ImageFile.CopyToAsync(fileStream);
        //            }
        //        }

        //        Image image = new Image
        //        {
        //            Title = model.Title,
        //            Description = model.Description,
        //            ImageCategoryId = model.ImageCategoryId,
        //            ImageUrl = "/assets/images/tara/" + uniqueFileName
        //        };

        //        _dbContext.Add(image);
        //        await _dbContext.SaveChangesAsync();

        //        return RedirectToAction(nameof(AdminGallery));
        //    }

        //    // If the model state is not valid, repopulate the ViewBag.Categories and return the view
        //    ViewBag.Categories = new SelectList(await _dbContext.ImageCategories.ToListAsync(), "Id", "Name");
        //    return View(model);
        //}


        public IActionResult AdminGallery()
        {
            var images = _dbContext.Images.Include(i => i.ImageCategory).ToList();
            ViewBag.Images = images;
            return View();
        }
    }
}

