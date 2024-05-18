using System.ComponentModel.DataAnnotations;

namespace Fiorello_Db.Areas.Admin.ViewModel.Blog
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(50, ErrorMessage = "Length must be max 50")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
    }
}
