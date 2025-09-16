using Microsoft.EntityFrameworkCore;

namespace Web.MVC.Redis.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Kalem 1", Price = 12 },
                new Product() { Id = 2, Name = "Kalem 2", Price = 11 },
                new Product() { Id = 3, Name = "Kalem 3", Price = 13 },
                new Product() { Id = 4, Name = "Kalem 4", Price = 15 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
