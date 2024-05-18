using Fiorello_Db.Models;

namespace Fiorello_Db.ViewModel
{
    public class HomeVM
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<CarouselImage> CarouselImages { get; set; }
    }
}
