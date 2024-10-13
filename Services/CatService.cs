using CatApiApp.Data;
using CatApiApp.Models;

namespace CatApiApp.Services
{
    /// <summary>
    /// Service for managing cat-related operations, including fetching and storing cat data from an external API.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CatService"/> class.
    /// </remarks>
    /// <param name="catApiClient">The client used to interact with the external Cat API.</param>
    /// <param name="context">The database context used to interact with the Cats and Tags entities.</param>
    public class CatService(ICatApiClient catApiClient, DataContext context)
    {
        private readonly ICatApiClient _catApiClient = catApiClient;
        private readonly DataContext _context = context;

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
                        Image = cat.Url,
                        Tags = ExtractTagsFromBreed(cat.Breeds)
                    };
                    _context.Cats.Add(newCat);
                }
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Extracts tags from cat breeds (temperament) and returns a list of associated tags.
        /// </summary>
        /// <param name="breeds">The list of breeds associated with the cat.</param>
        /// <returns>A list of <see cref="TagEntity"/> objects representing the cat's temperament.</returns>
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
