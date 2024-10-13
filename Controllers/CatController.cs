using CatApiApp.Data;
using CatApiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CatApiApp.Controllers
{
    /// <summary>
    /// Controller to handle cat-related API actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CatController"/> class.
    /// </remarks>
    /// <param name="catService">The service for handling cat-related operations.</param>
    /// <param name="context">The database context for interacting with the database.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class CatController(CatService catService, DataContext context) : ControllerBase
    {
        private readonly CatService _catService = catService;
        private readonly DataContext _context = context;

        /// <summary>
        /// Fetches 25 cat images from an external API and stores them in the database.
        /// </summary>
        /// <remarks>
        /// This method fetches cat images, including width, height, image URL, and associated tags,
        /// and stores them in the SQL database. If a cat already exists, it will not be added again.
        /// </remarks>
        /// <response code="200">Successfully fetched and stored cats</response>
        /// <response code="500">Internal server error while fetching or storing data</response>
        [HttpPost("fetch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FetchAndStoreCats()
        {
            try
            {
                // Fetch and store cats
                await _catService.FetchAndStoreCatsAsync();

                return Ok(new { Message = "Cats fetched and stored successfully." });
            }
            catch (ValidationException ex)
            {
                // If a validation exception occurs, return a bad request with details
                return BadRequest(new { ex.Message, Errors = ex.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a cat by its ID.
        /// </summary>
        /// <param name="id">The ID of the cat to retrieve</param>
        /// <response code="200">Returns the requested cat</response>
        /// <response code="404">If the cat is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCatById(int id)
        {
            var cat = await _context.Cats.Include(c => c.Tags).FirstOrDefaultAsync(c => c.Id == id);
            if (cat == null) return NotFound("Cat not found.");
            return Ok(cat);
        }

        /// <summary>
        /// Retrieve cats with paging support.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCats(int page = 1, int pageSize = 10, string tag = null)
        {
            var query = _context.Cats.AsQueryable();

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(c => c.Tags.Any(t => t.Name == tag));
            }

            var pagedCats = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(pagedCats);
        }
    }

}
