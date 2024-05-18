using Fiorello_Db.Areas.Admin.ViewModel.Category;
using Fiorello_Db.Models;

namespace Fiorello_Db.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<List<CategoryVM>> GetAllOrderByDescendingAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(CategoryCreateVM category);
        Task<Category> GetWithProductAsync(int id);
        Task<Category> GetByIdAsync(int id);
        Task DeleteAsync(Category category);
        Task EditAsync(Category category,CategoryEditVM categoryEdit);
    }
}
