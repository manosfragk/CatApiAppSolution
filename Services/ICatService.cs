namespace CatApiApp.Services
{
    public interface ICatService
    {
        Task FetchAndStoreCatsAsync();
    }
}
