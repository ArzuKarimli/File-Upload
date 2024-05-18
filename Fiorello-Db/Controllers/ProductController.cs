using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Fiorello_Db.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if(product is null ) return NotFound();
            return View(product);
        }

    }
}
