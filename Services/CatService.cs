using CatApiApp.Data;
using CatApiApp.Models;

namespace CatApiApp.Services
{
    public class CatService
    {
        private readonly ICatApiClient _catApiClient;
        private readonly DataContext _context;

        public CatService(ICatApiClient catApiClient, DataContext context)
        {
            _catApiClient = catApiClient;
            _context = context;
        }

        /// <summary>
        /// Fetches 25 cat images from the external API and stores them in the database.
        /// </summary>
        public async Task FetchAndStoreCatsAsync()
        {
            var catImages = await _catApiClient.FetchCatImagesAsync();

            foreach (var cat in catImages)
            {
                // Check if the cat already exists in the database by CatId
                if (!_context.Cats.Any(c => c.CatId == cat.Id))
                {
                    var newCat = new CatEntity
                    {
                        CatId = cat.Id,
                        Width = cat.Width,
                        Height = cat.Height,
                        Image = "URL??",// handle it
                        Tags = ExtractTagsFromBreed(cat.Breeds)
                    };
                    _context.Cats.Add(newCat);
                }
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Extracts tags from cat breeds (temperament).
        /// </summary>
        private List<TagEntity> ExtractTagsFromBreed(List<CatBreed> breeds)
        {
            var tags = new List<TagEntity>();
            foreach (var breed in breeds)
            {
                var temperaments = breed.Temperament.Split(',');
                foreach (var temperament in temperaments)
                {
                    var trimmedTemperament = temperament.Trim();
                    var tag = _context.Tags.FirstOrDefault(t => t.Name == trimmedTemperament);
                    if (tag == null)
                    {
                        tag = new TagEntity { Name = trimmedTemperament };
                        _context.Tags.Add(tag);
                    }
                    tags.Add(tag);
                }
            }
            return tags;
        }
    }

}
