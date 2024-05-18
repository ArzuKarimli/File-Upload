using Fiorello_Db.Models;

namespace Fiorello_Db.Services.Interfaces
{
    public interface IProductService
    { 
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
    }
}
