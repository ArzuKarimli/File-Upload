using System.ComponentModel.DataAnnotations;

namespace Fiorello_Db.Areas.Admin.ViewModel.Category
{
    public class CategoryEditVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }
    }
}
