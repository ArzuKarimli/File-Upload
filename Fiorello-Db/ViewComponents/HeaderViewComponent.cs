using Fiorello_Db.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fiorello_Db.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IBioService _bioService;

        public HeaderViewComponent(IBioService bioService)
        {
            _bioService = bioService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _bioService.GetAllAsync();
            return View(model);
        }
    }
}
