using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDlvAPI.Models;
using FoodDlvAPI.DTOs;
using Newtonsoft.Json;



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
		//1.01列出所有商店並且分頁顯示

		[HttpGet("getSomeStores")]
		public async Task<ActionResult<IEnumerable<StoreDTO>>> GetSomeStores(int storeNum, int pageNum)
		{
			var getSomeStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Select(x => new StoreDTO
			{
				Id = x.Id,
				StorePrincipalId = x.StorePrincipalId,
				StoreName = x.StoreName,
				Address = x.Address,
				ContactNumber = x.ContactNumber,
				Photo = x.Photo,


				CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)

			}).Skip((pageNum - 1) * storeNum).Take(storeNum).ToListAsync();
			return getSomeStores;
		}


		//1.02列出所有商店並且按照距離分頁顯示
		//
		[HttpGet("getSomeStoresIfIMAt/{origin}")]
		public async Task<ActionResult<IEnumerable<Store1DTO>>> GetSomeStoresIfIMAt(int pageNum, int storeNum, string origin)
		{
			var getSomeStores = await _context.Stores
				.Include(s => s.StoresCategoriesLists)
				.ThenInclude(x => x.Category)
				.Select(x => new Store1DTO
				{
					Id = x.Id,
					StorePrincipalId = x.StorePrincipalId,
					StoreName = x.StoreName,
					Address = x.Address,
					ContactNumber = x.ContactNumber,
					Photo = x.Photo,
					CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
				})
				.ToListAsync();

			var getSomeStoresWithDistance = new List<Store1DTO>();
			foreach (var store in getSomeStores)
			{
				var distance = await getDistance(store.Address, origin);
				store.Distance = distance;
				getSomeStoresWithDistance.Add(store);
			}

			var getSomeStoresOrderByDistance = getSomeStoresWithDistance.Where(x => x.Distance!=-1).OrderBy(x => x.Distance).Skip((pageNum - 1) * storeNum).Take(storeNum).ToList();
			return getSomeStoresOrderByDistance;
		}

		private async Task<int> getDistance(string storeAddress, string origin)
		{
			var Distance = -1;
			var apiKey =await _context.Apis.Where(x => x.Id == 1).Select(x => x.Apikey).FirstOrDefaultAsync();

			var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={storeAddress}&key={apiKey}&mode=walking";
			using var client = new HttpClient();
			var response = await client.GetAsync(url);
			var content = await response.Content.ReadAsStringAsync();
			if (content == null)
			{
				return Distance;
			}

			dynamic result = JsonConvert.DeserializeObject(content);
			if (result == null || result.rows == null || result.rows.Count == 0 || result.rows[0].elements == null || result.rows[0].elements.Count == 0 || result.rows[0].elements[0].distance == null)
			{
				Distance = -1;
			}
			else
			{
				Distance = result.rows[0].elements[0].distance.value;
			}

			return Distance;
		}



		//1.1列出所有類別
		// GET: api/StoreCategories
		[HttpGet("getStoreCategories")]
		public async Task<ActionResult<IEnumerable<StoreCategory>>> GetStoreCategories()
		{
			return await _context.StoreCategories.ToListAsync();
		}

        //2依類別選出商店，點選類別傳入類別ID顯示商店

        [HttpGet("getStoresByCategoryId/{categoryId?}")]
		public async Task<ActionResult<IEnumerable<StoreDTO>>> GetStores(int? categoryId)
		{
			if (categoryId != null)
			{
				var getStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Where(x => x.StoresCategoriesLists.Any(scl => scl.CategoryId == categoryId)).Select(x => new StoreDTO
				{
					Id = x.Id,
					StorePrincipalId = x.StorePrincipalId,
					StoreName = x.StoreName,
					Address = x.Address,
					ContactNumber = x.ContactNumber,
					Photo = x.Photo,

					CategoryName = x.StoresCategoriesLists
				  .Where(scl => scl.CategoryId == categoryId)
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



		//2.1依搜尋字串選出商店名稱、商店類別名稱、商店的商品名稱與搜尋字串有關的商店，如果商品狀態為FALSE則不列出
		[HttpGet("searchString")]
		public async Task<ActionResult<IEnumerable<StoreDTO>>> SearchStore(string? searchString)
		{
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

		[HttpGet("storeDetail/{storeId}")]

		public async Task<ActionResult<IEnumerable<StoreDetailDTO>>> GetStoreDetail(int storeId)
		{

			var getStoreDetail = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Include(x => x.Products).Where(x => x.Id == storeId).Select(x => new StoreDetailDTO
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

			return getStoreDetail;

		}




		//4店家擁有商店
		[HttpGet("myStores/{storePrincipalId}")]

		public async Task<ActionResult<IEnumerable<Store>>> GetMyStores(int storePrincipalId)
		{
			return await _context.Stores.Where(x => x.StorePrincipalId == storePrincipalId).ToListAsync();

		}


		//5店家擁有商店頁面

		[HttpGet("myStoreDetail/{storeId}")]

		public async Task<ActionResult<IEnumerable<MyStoreDetailDTO>>> GetMyStoreDetail(int storeId)
		{

			var getMyStoreDetail = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Where(x => x.StorePrincipalId == storeId).Include(x => x.Products).Select(x => new MyStoreDetailDTO
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

			return getMyStoreDetail;

		}

		//6商店內部資訊修改
		[HttpPut("{id}")]
		public async Task<string> PutStore(int id, Store store)
		{

			if (id != store.Id)
			{
				return "錯誤";
			}

			_context.Entry(store).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{

				if (!_context.Stores.Any(e => e.Id == id))
				{
					return "錯誤找不到此商店";
				}
				else
				{
					throw new Exception(ex.Message);
				}
			}

			return "修改成功";
		}


		//6.1商店標籤新增


		//6.2商店標籤刪除



		//7商店內部商品新增



		//7.1商店內部商品修改

		//7.2商店內部商品刪除

	}
}















