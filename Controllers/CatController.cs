using CatApiApp.Data;
using CatApiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatController : ControllerBase
    {
        private readonly CatService _catService;
        private readonly DataContext _context;

        public CatController(CatService catService, DataContext context)
        {
            _catService = catService;
            _context = context;
        }

        /// <summary>
        /// Fetch 25 cat images from the CaaS API and store them in the database.
        /// </summary>
        [HttpPost("fetch")]
        public async Task<IActionResult> FetchAndStoreCats()
        {
            await _catService.FetchAndStoreCatsAsync();
            return Ok("Cats fetched and stored successfully.");
        }

        /// <summary>
        /// Retrieve a cat by its ID.
        /// </summary>
        [HttpGet("{id}")]
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
