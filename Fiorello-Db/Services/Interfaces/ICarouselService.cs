using Fiorello_Db.Models;

namespace Fiorello_Db.Services.Interfaces
{
    public interface ICarouselService
    {
        Task<List<CarouselImage>> GetAllAsync();
    }
}
