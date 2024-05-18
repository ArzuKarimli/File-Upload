using Fiorello_Db.Areas.Admin.ViewModel.Category;
using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Fiorello_Db.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
       private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;
        public CategoryController( AppDbContext context,ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {          
            return View(await _categoryService.GetAllOrderByDescendingAsync());
        }

        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create(CategoryCreateVM category)
        {

            if(!ModelState.IsValid)
            {
                return View();
            }

            bool existCategory = await _categoryService.ExistAsync(category.Name);

            if (existCategory)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }
            await _categoryService.CreateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetWithProductAsync((int)id);

            if(category is null) return NotFound();

            CategoryDetailVM model = new()
            {
                Name = category.Name,
                ProductCount = category.Products.Count
            };
            return View( model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetWithProductAsync((int)id);

            if (category is null) return NotFound();
            _categoryService.DeleteAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetWithProductAsync((int)id);

            if (category is null) return NotFound();
            return View(new CategoryEditVM { Id=category.Id,Name=category.Name});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,CategoryEditVM category)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();

            Category existCategory = await _categoryService.GetByIdAsync((int)id);
            bool existCategoryName = await _categoryService.ExistAsync(existCategory.Name);

            if (existCategoryName)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }
            await _categoryService.EditAsync(existCategory, category);
            if (existCategory is null) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}