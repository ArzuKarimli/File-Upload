using Fiorello_Db.Areas.Admin.ViewModel.Blog;
using Fiorello_Db.Models;

namespace Fiorello_Db.Services.Interfaces
{
    public  interface IBlogService 
    {
        Task<List<BlogVM>> GetAllOrderByDescAsync();
        Task CreateAsync(BlogCreateVM blog);
        Task<bool> ExistAsync(string title,string desc);
        Task<bool> ExistForTitleAsync(string title);
        Task<Blog> GetByIdAsync(int id);
        Task DeleteAsync(Blog blog);
        Task EditAsync(Blog blog,BlogEditVM blogEdit);
    }
}
