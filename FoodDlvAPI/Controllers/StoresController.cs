using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDlvAPI.Models;
using FoodDlvAPI.DTOs;

namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StoresController(AppDbContext context)
        {
            _context = context;
		}



		//2依類別選出商店，點選類別傳入類別ID顯示商店

		[HttpGet("getStoresByCategoryId/{CategoryId?}")]
		public async Task<ActionResult<IEnumerable<StoreDTO>>> GetStores(int? CategoryId)
		{
			if (CategoryId != null)
			{
				var getStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Where(x => x.StoresCategoriesLists.Any(scl => scl.CategoryId == CategoryId)).Select(x => new StoreDTO
				{
					Id = x.Id,
					StorePrincipalId=x.StorePrincipalId,
					StoreName = x.StoreName,
                    Address= x.Address,
                    ContactNumber= x.ContactNumber,
					Photo = x.Photo,

					CategoryName = x.StoresCategoriesLists
				  .Where(scl => scl.CategoryId == CategoryId)
				  .Select(scl => scl.Category.CategoryName)
				})
				  .ToListAsync();

				return getStores;
			}
			else
			{
				var getAllStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Select(x => new StoreDTO
				{
					Id = x.Id,
					StorePrincipalId = x.StorePrincipalId,
					StoreName = x.StoreName,
					Address = x.Address,
					ContactNumber = x.ContactNumber,
					Photo = x.Photo,


					CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)

				}).ToListAsync();
				return getAllStores;
			}
		}



		//2.1依搜尋字串選出商店名稱、商店類別名稱、商店的商品名稱與搜尋字串有關的商店
		[HttpGet("search")]
		public async Task<ActionResult<IEnumerable<StoreDTO>>> SearchStore(string? searchString)
		{

			var searchStores = await _context.Stores.Include(x => x.StoresCategoriesLists).ThenInclude(x => x.Category).Include(x => x.Products).
						   Where(s => s.StoreName.Contains(searchString)
						   || s.StoresCategoriesLists.Any(scl => scl.Category.CategoryName.Contains(searchString))
						   || s.Products.Any(p => p.ProductName.Contains(searchString))).Select(x => new StoreDTO
						   {
							   Id = x.Id,
							   StorePrincipalId = x.StorePrincipalId,
							   StoreName = x.StoreName,
							   Address = x.Address,
							   ContactNumber = x.ContactNumber,
							   Photo = x.Photo,

							   CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)
						   }).ToListAsync();

			return searchStores;

		}































		// GET: api/Stores
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            return await _context.Stores.ToListAsync();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, Store store)
        {
            if (id != store.Id)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStore", new { id = store.Id }, store);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
