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
    public class StoresCategoriesListsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StoresCategoriesListsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StoresCategoriesLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoresCategoriesList>>> GetStoresCategoriesLists()
        {
            return await _context.StoresCategoriesLists.ToListAsync();
        }

        // GET: api/StoresCategoriesLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoresCategoriesList>> GetStoresCategoriesList(int id)
        {
            var storesCategoriesList = await _context.StoresCategoriesLists.FindAsync(id);

            if (storesCategoriesList == null)
            {
                return NotFound();
            }

            return storesCategoriesList;
        }

        // PUT: api/StoresCategoriesLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoresCategoriesList(int id, StoresCategoriesList storesCategoriesList)
        {
            if (id != storesCategoriesList.Id)
            {
                return BadRequest();
            }

            _context.Entry(storesCategoriesList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoresCategoriesListExists(id))
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

        // POST: api/StoresCategoriesLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoresCategoriesList>> PostStoresCategoriesList(StoresCategoriesList storesCategoriesList)
        {
            _context.StoresCategoriesLists.Add(storesCategoriesList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoresCategoriesList", new { id = storesCategoriesList.Id }, storesCategoriesList);
        }

        // DELETE: api/StoresCategoriesLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoresCategoriesList(int id)
        {
            var storesCategoriesList = await _context.StoresCategoriesLists.FindAsync(id);
            if (storesCategoriesList == null)
            {
                return NotFound();
            }

            _context.StoresCategoriesLists.Remove(storesCategoriesList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoresCategoriesListExists(int id)
        {
            return _context.StoresCategoriesLists.Any(e => e.Id == id);
        }
    }
}
