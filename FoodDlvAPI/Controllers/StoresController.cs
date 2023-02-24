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

		//1列出所有商店
		// GET: api/Stores
		[HttpGet]
		public async Task<ActionResult<IEnumerable<StoreDTO>>> GetStores()
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




		//1.1列出所有類別
		// GET: api/StoreCategories
		[HttpGet("getStoreCategories")]
		public async Task<ActionResult<IEnumerable<StoreCategory>>> GetStoreCategories()
		{
			return await _context.StoreCategories.ToListAsync();
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
					StorePrincipalId = x.StorePrincipalId,
					StoreName = x.StoreName,
					Address = x.Address,
					ContactNumber = x.ContactNumber,
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

			//var searchStores = await _context.Stores.Include(x => x.StoresCategoriesLists).ThenInclude(x => x.Category).Include(x => x.Products).
			//			   Where(s => s.StoreName.Contains(searchString)
			//			   || s.StoresCategoriesLists.Any(scl => scl.Category.CategoryName.Contains(searchString))
			//			   || s.Products.Any(p => p.ProductName.Contains(searchString))).Select(x => new StoreDTO
			//			   {
			//				   Id = x.Id,
			//				   StorePrincipalId = x.StorePrincipalId,
			//				   StoreName = x.StoreName,
			//				   Address = x.Address,
			//				   ContactNumber = x.ContactNumber,
			//				   Photo = x.Photo,

			//				   CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)
			//			   }).ToListAsync();

			//return searchStores;


			var searchStores = await _context.Stores.Include(x => x.StoresCategoriesLists).ThenInclude(x => x.Category).Include(x => x.Products).
						   Where(s => s.StoreName.Contains(searchString)
						   || s.StoresCategoriesLists.Any(scl => scl.Category.CategoryName.Contains(searchString))
						   || s.Products.Any(p => p.ProductName.Contains(searchString) && s.Products.Any(p => p.Status == true))).Select(x => new StoreDTO
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



		//3進入商店頁面顯示其資訊包含商品資訊

		[HttpGet("StoreDetail/{StoreId}")]

		public async Task<ActionResult<IEnumerable<StoreDetailDTO>>> GetStoreDetail(int StoreId)
		{

			var getStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Where(x => x.Id == StoreId).Select(x => new StoreDetailDTO
			{
				Id = x.Id,
				StorePrincipalId = x.StorePrincipalId,
				StoreName = x.StoreName,
				Address = x.Address,
				ContactNumber = x.ContactNumber,
				Photo = x.Photo,

				CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)

			   ,
				ProductId = x.Products.Select(x => x.Id)
			   ,
				ProductName = x.Products.Select(x => x.ProductName)
			   ,
				ProductPhoto = x.Products.Select(x => x.Photo)
				,
				ProductContent = x.Products.Select(x => x.ProductContent)
				,
				ProductStatus = x.Products.Select(x => x.Status)
				,
				ProductUnitPrice = x.Products.Select(x => x.UnitPrice)

			})
				  .ToListAsync();

			return getStores;

		}
	}
}
