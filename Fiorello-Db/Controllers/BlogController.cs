using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Controllers
{
    public class BlogController : Controller
    {
        public AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() {


            int count = await _context.Blogs.CountAsync();
            ViewBag.Count = count;
            return View(await _context.Blogs.Where(m => !m.SoftDeleted).Take(3).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ShowMore(int skip)
        {
            List<Blog> blogs= await _context.Blogs.Skip(skip).Take(3).ToListAsync();
            return PartialView("_BlogsPartial",blogs);
        }
    }
}
