namespace Fiorello_Db.Services.Interfaces
{
    public interface IBioService
    {
        Task<Dictionary<string,string>> GetAllAsync();
    }
}
