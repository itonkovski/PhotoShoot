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
using PhotoShoot.Services.Images;

namespace PhotoShoot.Controllers
{
	public class ImagesController : Controller
	{
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;

        public ImagesController(IWebHostEnvironment hostEnvironment, ApplicationDbContext dbContext, IImageService imageService)
		{
            _hostEnvironment = hostEnvironment;
            _dbContext = dbContext;
            _imageService = imageService;
        }        

        public IActionResult CreateImage()
        {
            //When using only ViewBag
            //var model = new ImageFormModel { Categories = categories };
            //return View(model);
            //var model = new ImageFormModel
            //{
            //    Categories = _dbContext.ImageCategories
            //        .Select(c => new ImageCategoryViewModel { ImageCategoryId = c.Id, Name = c.Name })
            //        .ToList()
            //};

            ViewBag.Images = _dbContext.Images.Include(i => i.ImageCategory).ToList();
            //return View(model);
            return View(new ImageFormModel
            {
                Categories = GetImageCategories()
            });
        }

        /*
        //With ImageSharp -> not tested yet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImageAsync(ImageFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = GetImageCategories();
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
                // Load the image from the uploaded file
                using (var image = ImageSharpImage.Load(model.ImageFile.OpenReadStream()))
                {
                    // Resize the image
                    int desiredWidth = 1200;
                    int desiredHeight = 1200;

                    var resizeOptions = new ResizeOptions
                    {
                        Size = new Size(desiredWidth, desiredHeight),
                        Mode = ResizeMode.Max
                    };
                    image.Mutate(x => x.Resize(resizeOptions));

                    // Save the resized image
                    image.Save(fileStream, new JpegEncoder());
                }
            }

            // Save the image record to the database
            var imageRecord = new Image
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = "/assets/images/tara/" + uniqueFileName,
                ImageCategoryId = model.ImageCategoryId,
            };

            _dbContext.Images.Add(imageRecord);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("AdminGallery", "Images");
        }
        */


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImageAsync(ImageFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = GetImageCategories();
                return View(model);
            }

            await _imageService.CreateAsync(model, _hostEnvironment.WebRootPath);

            return RedirectToAction("AdminGallery", "Images");
        }



        //With ViewBag -> working properly
        public IActionResult AdminGallery()
        {
            var images = _dbContext.Images.Include(i => i.ImageCategory).ToList();
            ViewBag.Images = images;
            return View();
        }


        //With ImageViewModel + Service -> working properly but not having the image structure
        //public IActionResult AdminGallery()
        //{
        //    var images = _imageService.GetAllImages();
        //    return View(images);
        //}

        //With AllImagesViewModel + Service-> working properly
        //public IActionResult AdminGallery(AllImagesViewModel model)
        //{
        //    _imageService.AllImages(model);
        //    return View(model);
        //}

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

