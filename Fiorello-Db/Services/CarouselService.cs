using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Fiorello_Db.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Services
{
    public class CarouselService : ICarouselService
    {
        private readonly AppDbContext _context;
        public CarouselService(AppDbContext context)
        {
            _context = context;

        }
        public async Task<List<CarouselImage>> GetAllAsync()
        {
           return await _context.CarouselImages.ToListAsync();
        }
    }
}
