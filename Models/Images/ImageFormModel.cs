using System;
using PhotoShoot.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PhotoShoot.Models.Images
{
    public class ImageFormModel
	{
        //public string Id { get; set; }
        
        [Display(Name = "Image Title")]
        public string Title { get; set; }

        //public string ImageUrl { get; set; }

        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image Category")]
        public int ImageCategoryId { get; set; }

        public IEnumerable<ImageCategoryViewModel> Categories { get; set; } = new List<ImageCategoryViewModel>();
    }
}

