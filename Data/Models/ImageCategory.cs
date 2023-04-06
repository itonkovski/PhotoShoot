using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PhotoShoot.Data.Models
{
	public class ImageCategory
	{
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}

