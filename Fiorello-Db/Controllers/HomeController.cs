
using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Fiorello_Db.Services.Interfaces;
using Fiorello_Db.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Diagnostics;

namespace Fiorello_Db.Controllers
{
    public class HomeController : Controller
    {
       private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICarouselService _carouselService;
        public HomeController( AppDbContext context,IProductService productService,ICategoryService categoryService,ICarouselService carouselService)
        {
            _context = context;
             _productService=productService;
            _categoryService=categoryService;
            _carouselService=carouselService;
        }

        public async Task<IActionResult> Index()
        {
           
            List<Category> categories= await _categoryService.GetAllAsync();
            List<Product> products= await _productService.GetAllAsync();
            List<Blog> blogs= await _context.Blogs.Where(m=>!m.SoftDeleted).Take(3).ToListAsync();
            List<CarouselImage> carouselImages = await _carouselService.GetAllAsync();
            HomeVM model = new()
            {
              Blogs = blogs,
              Products = products,
              Categories= categories,
              CarouselImages= carouselImages
            };
           
            
             return View(model);
        }
    }
}