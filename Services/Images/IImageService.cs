using System;
using PhotoShoot.Models.Images;

namespace PhotoShoot.Services.Images
{
	public interface IImageService
	{
		IEnumerable<ImageViewModel> GetAllImages();

		public void AllImages(AllImagesViewModel model);
	}
}

