using System.ComponentModel.DataAnnotations;

namespace Fiorello_Db.Areas.Admin.ViewModel.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public List< IFormFile> Images  { get; set; }
    }
}
