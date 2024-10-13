using Refit;

namespace CatApiApp.Services
{
    public interface ICatApiClient
    {
        [Get("/v1/images/search?limit=25&has_breeds=true")]
        Task<List<CatApiResponse>> FetchCatImagesAsync();
    }

    public class CatApiResponse
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<CatBreed> Breeds { get; set; }
    }

    public class CatBreed
    {
        public string Temperament { get; set; }
    }
}
