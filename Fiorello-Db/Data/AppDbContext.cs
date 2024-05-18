using Fiorello_Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
        
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blogs { get; set; }  
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CarouselImage> CarouselImages { get; set; }
        public DbSet<Bio> Bios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasQueryFilter(x => !x.SoftDeleted);
            modelBuilder.Entity<Blog>()
                        .HasData(
           new Blog
           {
               Id = 1,
               Title = "Blog Title1",
               Description = "Description1",
               Date= DateTime.Now,
               Image= "blog-feature-img-1.jpg"
           },
          new Blog
          {
              Id = 2,
              Title = "Blog Title2",
              Description = "Description2",
              Date = DateTime.Now,
              Image = "blog-feature-img-3.jpg"
          },
          new Blog
          {
              Id = 3,
              Title = "Blog Title3",
              Description = "Description3",
              Date = DateTime.Now,
              Image = "blog-feature-img-4.jpg"
          }
         );
        }

    }
}
