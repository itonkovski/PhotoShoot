using System;
using PhotoShoot.Data;
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
                    ImageCategory = x.ImageCategory.ToString()
                });
            model.Images = images;
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
    }
}

