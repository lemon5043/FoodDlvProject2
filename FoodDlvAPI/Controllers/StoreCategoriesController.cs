using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDlvAPI.Models;

namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreCategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StoreCategoriesController(AppDbContext context)
        {
            _context = context;
		}



        //1列出所有類別
		// GET: api/StoreCategories
		[HttpGet]
        public async Task<ActionResult<IEnumerable<StoreCategory>>> GetStoreCategories()
        {
            return await _context.StoreCategories.ToListAsync();
        }



















        // GET: api/StoreCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreCategory>> GetStoreCategory(int id)
        {
            var storeCategory = await _context.StoreCategories.FindAsync(id);

            if (storeCategory == null)
            {
                return NotFound();
            }

            return storeCategory;
        }

        // PUT: api/StoreCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreCategory(int id, StoreCategory storeCategory)
        {
            if (id != storeCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(storeCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreCategoryExists(id))
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

        // POST: api/StoreCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoreCategory>> PostStoreCategory(StoreCategory storeCategory)
        {
            _context.StoreCategories.Add(storeCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoreCategory", new { id = storeCategory.Id }, storeCategory);
        }

        // DELETE: api/StoreCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreCategory(int id)
        {
            var storeCategory = await _context.StoreCategories.FindAsync(id);
            if (storeCategory == null)
            {
                return NotFound();
            }

            _context.StoreCategories.Remove(storeCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreCategoryExists(int id)
        {
            return _context.StoreCategories.Any(e => e.Id == id);
        }
    }
}
