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
    }
}

