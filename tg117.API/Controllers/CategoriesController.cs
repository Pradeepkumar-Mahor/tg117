using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tg117.Domain;
using tg117.Domain.DbContext;
using static tg117.API.Classes.GenericClass;

namespace tg117.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategory([FromBody] QueryParams queryParams)
        {
            if (queryParams == null)
            {
                return BadRequest("User not found");
            }
            List<Category> query = await _context.Category.ToListAsync();

            PagedList<Category> retsult =
                            await PagedList<Category>.ReturnListAsync
                                (
                                    query, queryParams.PageNumber, queryParams.PageSize)
                                ;

            return new JsonResult(retsult);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            Category? category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Guid)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (category.Guid == null)
            {
                category.Guid = Guid.NewGuid();
            }
            _ = _context.Category.Add(category);
            _ = await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Guid }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            Category? category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _ = _context.Category.Remove(category);
            _ = await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.Guid == id);
        }
    }
}