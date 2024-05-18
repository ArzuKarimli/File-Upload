using Fiorello_Db.Data;
using Fiorello_Db.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Fiorello_Db.Services
{
    public class BioService : IBioService
    {
        private readonly AppDbContext _context;
        public BioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetAllAsync()
        {
           return await _context.Bios.ToDictionaryAsync(m=>m.Key, m=>m.Value);
           
        }
    }
}
