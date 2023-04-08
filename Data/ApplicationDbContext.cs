using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoShoot.Data.Models;

namespace PhotoShoot.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Image> Images { get; set; }

    public DbSet<ImageCategory> ImageCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed hardcoded ImageCategory data
        modelBuilder.Entity<ImageCategory>().HasData(
            new ImageCategory { Id = 1, Name = "Urban" },
            new ImageCategory { Id = 2, Name = "Nature" },
            new ImageCategory { Id = 3, Name = "Architecture" },
            new ImageCategory { Id = 4, Name = "Portrait" }
        // Add more categories as needed
        );

        // Seed hardcoded Images data
        //modelBuilder.Entity<Image>().HasData(
        //    new Image { Id = Guid.NewGuid().ToString(), Title = "Image 1", Description = "Image 1 description", ImageUrl = "../assets/images/tara/road_forest.jpg", ImageCategoryId = 1 },
        //    new Image { Id = Guid.NewGuid().ToString(), Title = "Image 2", Description = "Image 2 description", ImageUrl = "../assets/images/tara/forest_grave.jpg", ImageCategoryId = 2 }
        //// Add more images as needed
        //);
    }
}

