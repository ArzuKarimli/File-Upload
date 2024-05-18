using Fiorello_Db.Areas.Admin.Helpers.Extentions;
using Fiorello_Db.Areas.Admin.ViewModel.Sliders;
using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders=  await _context.Sliders.ToListAsync();
            List<SliderVM> result = sliders.Select(m=> new SliderVM { Id=m.Id,Image=m.Image}).ToList();

            return View( result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "File must be only image format");
                }

                if (!item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 kb");
                }

            }
            foreach (var item in request.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "img", fileName);

                await item.SaveFileToLocalAsync(path);
                await _context.Sliders.AddAsync(new Slider { Image = fileName });
                await _context.SaveChangesAsync();
            }

          
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
           Slider slider= await _context.Sliders.Where(m=>m.Id == id).FirstOrDefaultAsync();
            if(slider == null) return NotFound(); 

            return View(new SliderDetailVM { Image=slider.Image});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Slider slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (slider == null) return NotFound();

             _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
