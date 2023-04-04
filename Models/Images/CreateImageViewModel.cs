using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhotoShoot.Models.Images
{
	public class CreateImageViewModel
	{
        public ImageFormModel ImageForm { get; set; }

        public SelectList ImageCategories { get; set; }
    }
}

