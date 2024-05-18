using Fiorello_Db.Areas.Admin.ViewModel.Blog;
using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Fiorello_Db.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        public BlogController(AppDbContext context,IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
                return View( await _blogService.GetAllOrderByDescAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blogCreate)
        {
            if(ModelState.IsValid)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                return View();
            }

            bool existBlog = await _blogService.ExistAsync(blogCreate.Title, blogCreate.Description);
            if (existBlog)
            {
                ModelState.AddModelError("Title", "A blog with the same title and description already exists.");
                ModelState.AddModelError("Description", "A blog with the same title and description already exists.");
                return View();
            }

            _blogService.CreateAsync(blogCreate);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Blog blog= await _blogService.GetByIdAsync((int)id);
            if (blog == null) NotFound();

            BlogDetailVM model = new()
            {
                Title = blog.Title,
                Description = blog.Description,
                Date = blog.Date,
                Image = blog.Image,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null) return BadRequest();

            Blog blog = await _blogService.GetByIdAsync((int)id);
            if (blog == null) NotFound();

            await _blogService.DeleteAsync(blog);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Blog blog = await _blogService.GetByIdAsync((int)id);
            if (blog == null) return NotFound();

            return View(new BlogEditVM { Id = blog.Id, Title = blog.Title, Description = blog.Description, Date = blog.Date });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,BlogEditVM blogEdit)
        {
         
            if (id is null) return BadRequest();

            Blog dbBlog = await _blogService.GetByIdAsync((int)id);
            if (dbBlog == null) NotFound();
            bool existBlog = await _blogService.ExistForTitleAsync(blogEdit.Title);
            if (existBlog)
            {
               
                ModelState.AddModelError("Title", "A blog with the same title and description already exists.");
                return View();
            }

            await _blogService.EditAsync(dbBlog,blogEdit);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
