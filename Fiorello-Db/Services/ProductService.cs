using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Fiorello_Db.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync()
        {
           return await _context.Products.Include(m => m.ProductImages).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Where(m => !m.SoftDeleted)
                                                     .Include(m => m.ProductImages)
                                                     .Include(m => m.Category)
                                                     .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
