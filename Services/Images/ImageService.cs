using System;
using PhotoShoot.Data;
using PhotoShoot.Data.Models;
using PhotoShoot.Models.Images;

namespace PhotoShoot.Services.Images
{
	public class ImageService : IImageService
	{
        private readonly ApplicationDbContext _dbContext;

        public ImageService(ApplicationDbContext dbContext)
		{
            _dbContext = dbContext;
        }

        public void AllImages(AllImagesViewModel model)
        {
            var imagesQuery = _dbContext.Images.AsQueryable();
            var images = imagesQuery
                .Select(x => new ImageViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    ImageCategory = x.ImageCategory.Name
                });
            model.Images = images;
        }

        public async Task CreateAsync(ImageFormModel model, string webRootPath)
        {
            // Save the uploaded file
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
            var uploadsFolder = Path.Combine(webRootPath, "assets/images/tara");

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
        }

        public IEnumerable<ImageViewModel> GetAllImages()
        {
            var images =  _dbContext
                .Images
                .Select(image => new ImageViewModel
                {
                    Id = image.Id,
                    Title = image.Title,
                    ImageUrl = image.ImageUrl,
                    Description = image.Description,
                    ImageCategory = image.ImageCategory.ToString()
                })
                .ToList();

            return images;
        }

        public IEnumerable<ImageCategoryViewModel> GetImageCategories()
        {
            var categories = this._dbContext
                .ImageCategories
                .Select(x => new ImageCategoryViewModel
                {
                    ImageCategoryId = x.Id,
                    Name = x.Name
                })
                .ToList();

            return categories;
    }   }
}

