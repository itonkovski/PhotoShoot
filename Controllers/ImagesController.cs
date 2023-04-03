using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PhotoShoot.Data;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin, User")]
        private async Task<string> CreateImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(100).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        public IActionResult AdminGallery()
        {
            var images = _dbContext.Images.Include(i => i.ImageCategory).ToList();
            ViewBag.Images = images;
            return View();
        }
    }
}

