using Fiorello_Db.Areas.Admin.ViewModel.Blog;
using Fiorello_Db.Data;
using Fiorello_Db.Models;
using Fiorello_Db.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Db.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(BlogCreateVM blog)
        {
            await _context.Blogs.AddAsync(new Blog { Date = blog.Date, Description = blog.Description, Title = blog.Title, Image = "blog-feature-img-3.jpg" });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Blog blog)
        {
           _context.Blogs.Remove(blog);
           await  _context.SaveChangesAsync();
        }

        public async Task EditAsync(Blog blog, BlogEditVM blogEdit)
        {
            blog.Title = blogEdit.Title;
            blog.Description = blogEdit.Description;
            blog.Date = blogEdit.Date;
            await _context.SaveChangesAsync(); 
        }


        public async Task<bool> ExistAsync(string title, string desc)
        {
            return await _context.Blogs.AnyAsync(m => m.Title == title || m.Description == desc );
        }

        public  async Task<bool> ExistForTitleAsync(string title)
        {
            return await _context.Blogs.AnyAsync(m => m.Title == title);
        }

        public async Task<List<BlogVM>> GetAllOrderByDescAsync()
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.Id).ToListAsync();

            return  blogs.Select(m => new BlogVM { Id = m.Id, Title = m.Title, Description = m.Description, Date = m.Date, Image = m.Image }).ToList();
        }


        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.Where(m => m.Id == id).FirstOrDefaultAsync();
        }
    }
}
