using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PhotoShoot.Data.Models
{
	public class Image
	{
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        
        [Display(Name = "Image Title")]
        public string Title { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image Category")]
        public int ImageCategoryId { get; set; }

        public ImageCategory ImageCategory { get; set; }
    }
}

