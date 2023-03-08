using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDlvAPI.Models;
using Newtonsoft.Json;
using FoodDlvAPI.Models.DTOs;

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

		/// <summary>
		/// 傳入搜尋字串、頁碼、顯示數量、所在地點、餐廳類別ID，先依據搜尋字串與類別ID選出商店名稱、商店類別名稱、商店的商品名稱與搜尋字串有關的商店
		/// ，如果與搜尋字串有關的商品狀態為FALSE則不列出，然後利用所在地與商店的經緯度計算距離後依距排列(如果沒傳入類別ID則顯示全部餐廳)
		/// </summary>
		/// <param name="pageNum">頁碼</param>
		/// <param name="storeNum">顯示數量</param>
		/// <param name="origin">所在地點</param>
		/// <param name="categoryId">餐廳類別ID</param>
		/// <param name="searchString">搜尋字串</param>
		/// <returns>List<StoreGetDTO>getSomeStoresOrderByDistance</returns>


		[HttpGet("getSomeStoresIfIMAt/{origin}")]
		public async Task<ActionResult<IEnumerable<StoreGetDTO>>> GetSomeThisCategoryOfStoresIfIMAt(int pageNum, int storeNum, string origin, int? categoryId, string? searchString)
		{
			List<StoreGetDTO> getSomeStores;
			//如果有搜尋字串
			if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrWhiteSpace(searchString))
			{
				//如果有搜尋字串有類別ID
				if (categoryId != null)
				{

					getSomeStores = await _context.Stores.Include(x => x.StoresCategoriesLists).ThenInclude(x => x.Category).Include(x => x.Products).
				   Where(s => s.StoreName.Contains(searchString)
				   || s.StoresCategoriesLists.Any(scl => scl.Category.CategoryName.Contains(searchString))
				   || s.Products.Any(p => p.ProductName.Contains(searchString) && s.Products.Any(p => p.Status == true))).Where(x => x.StoresCategoriesLists.Any(scl => scl.CategoryId
				   == categoryId)).Select(x => new StoreGetDTO
				   {
					   Id = x.Id,
					   StorePrincipalId = x.StorePrincipalId,
					   StoreName = x.StoreName,
					   Address = x.Address,
					   ContactNumber = x.ContactNumber,
					   Photo = x.Photo,
					   CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
					   Longitude = x.Longitude,
					   Latitude = x.Latitude,
				   }).ToListAsync();
				}
				//如果有搜尋字串沒有類別ID
				else
				{
					getSomeStores = await _context.Stores.Include(x => x.StoresCategoriesLists).ThenInclude(x => x.Category).Include(x => x.Products).
				   Where(s => s.StoreName.Contains(searchString)
				   || s.StoresCategoriesLists.Any(scl => scl.Category.CategoryName.Contains(searchString))
				   || s.Products.Any(p => p.ProductName.Contains(searchString) && s.Products.Any(p => p.Status == true))).Select(x => new StoreGetDTO
				   {
					   Id = x.Id,
					   StorePrincipalId = x.StorePrincipalId,
					   StoreName = x.StoreName,
					   Address = x.Address,
					   ContactNumber = x.ContactNumber,
					   Photo = x.Photo,
					   CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
					   Longitude = x.Longitude,
					   Latitude = x.Latitude,
				   }).ToListAsync();
				}
			}
			//如果沒有搜尋字串
			else
			{
				//如果沒有搜尋字串有類別ID
				if (categoryId != null)
				{
					getSomeStores = await _context.Stores
						.Include(s => s.StoresCategoriesLists)
						.ThenInclude(x => x.Category)
						.Where(x => x.StoresCategoriesLists.Any(scl => scl.CategoryId == categoryId))
						.Select(x => new StoreGetDTO
						{
							Id = x.Id,
							StorePrincipalId = x.StorePrincipalId,
							StoreName = x.StoreName,
							Address = x.Address,
							ContactNumber = x.ContactNumber,
							Photo = x.Photo,
							CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
							Longitude = x.Longitude,
							Latitude = x.Latitude,

						})
						.ToListAsync();
				}
				//如果沒有搜尋字串沒有類別ID
				else
				{
					getSomeStores = await _context.Stores
						.Include(s => s.StoresCategoriesLists)
						.ThenInclude(x => x.Category)
						.Select(x => new StoreGetDTO
						{
							Id = x.Id,
							StorePrincipalId = x.StorePrincipalId,
							StoreName = x.StoreName,
							Address = x.Address,
							ContactNumber = x.ContactNumber,
							Photo = x.Photo,
							CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
							Longitude = x.Longitude,
							Latitude = x.Latitude,
						})
						.ToListAsync();
				}
			}
			var OriginsLongitudeNLatitude = await getOriginsLongitudeNLatitude(origin);
			var getSomeStoresWithDistance = new List<StoreGetDTO>();

			foreach (var store in getSomeStores)
			{
				var distance = await getDistance(store.Longitude, store.Latitude, OriginsLongitudeNLatitude[0], OriginsLongitudeNLatitude[1]);
				store.Distance = distance;
				getSomeStoresWithDistance.Add(store);
			}

			var getSomeStoresOrderByDistance = getSomeStoresWithDistance.Where(x => x.Distance != -1).OrderBy(x => x.Distance).Skip((pageNum - 1) * storeNum).Take(storeNum).ToList();
			return getSomeStoresOrderByDistance;
		}

		//取得所在地經緯度
		private async Task<List<double>> getOriginsLongitudeNLatitude(string origin)
		{
			var apiKey = await _context.Apis.Where(x => x.Id == 1).Select(x => x.Apikey).FirstOrDefaultAsync();
			var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={origin}&key={apiKey}";
			using var client = new HttpClient();
			var response = await client.GetAsync(url);
			var content = await response.Content.ReadAsStringAsync();
			dynamic result = JsonConvert.DeserializeObject(content);

			var OriginsLongitude = Convert.ToDouble(result.results[0].geometry.location.lng);
			var OriginsLatitude = Convert.ToDouble(result.results[0].geometry.location.lat);
			var OriginsLongitudeNLatitude = new List<double>() { OriginsLongitude, OriginsLatitude };

			return OriginsLongitudeNLatitude;
		}
		//取得店家與所在地距離
		private async Task<double> getDistance(double storeLng, double storeLat, double originLng, double originLat)
		{
			double R = 6371; // 地球平均半徑，單位為公里
			double dLat = Math.Abs(storeLat - originLat) * Math.PI / 180;
			double dLon = Math.Abs(storeLng - originLng) * Math.PI / 180;
			double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(originLat * Math.PI / 180) * Math.Cos(storeLat * Math.PI / 180) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			double distance_km = R * c;



			//double dLat = Math.Abs(storeLat - originLat);
			//double dLon = Math.Abs(storeLng - originLng);

			//var distance_km = Math.Sqrt(Math.Pow(dLon, 2) + Math.Pow(dLat, 2)) * 110.9362;


			return distance_km;

		}







		////傳入頁碼、顯示數量、所在地點、餐廳類別ID，然後利用經緯計算距離後依距排列(如果沒傳入類別ID則顯示全部餐廳)
		//[HttpGet("getSomeStoresIfIMAt/{origin}/{categoryId?}")]
		//public async Task<ActionResult<IEnumerable<Store2DTO>>> GetSomeThisCategoryOfStoresIfIMAt(int pageNum, int storeNum, string origin, int? categoryId)
		//{
		//	List<Store2DTO> getSomeStores;


		//	if (categoryId != null)
		//	{
		//		getSomeStores = await _context.Stores
		//			.Include(s => s.StoresCategoriesLists)
		//			.ThenInclude(x => x.Category)
		//			.Where(x => x.StoresCategoriesLists.Any(scl => scl.CategoryId == categoryId))
		//			.Select(x => new Store2DTO
		//			{
		//				Id = x.Id,
		//				StorePrincipalId = x.StorePrincipalId,
		//				StoreName = x.StoreName,
		//				Address = x.Address,
		//				ContactNumber = x.ContactNumber,
		//				Photo = x.Photo,
		//				CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
		//				Longitude = x.Longitude,
		//				Latitude = x.Latitude,

		//			})
		//			.ToListAsync();
		//	}
		//	else
		//	{
		//		getSomeStores = await _context.Stores
		//			.Include(s => s.StoresCategoriesLists)
		//			.ThenInclude(x => x.Category)
		//			.Select(x => new Store2DTO
		//			{
		//				Id = x.Id,
		//				StorePrincipalId = x.StorePrincipalId,
		//				StoreName = x.StoreName,
		//				Address = x.Address,
		//				ContactNumber = x.ContactNumber,
		//				Photo = x.Photo,
		//				CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
		//				Longitude = x.Longitude,
		//				Latitude = x.Latitude,
		//			})
		//			.ToListAsync();
		//	}

		//	var OriginsLongitudeNLatitude = await getOriginsLongitudeNLatitude(origin);
		//	var getSomeStoresWithDistance = new List<Store2DTO>();

		//	foreach (var store in getSomeStores)
		//	{

		//		var distance = await getDistance(store.Longitude, store.Latitude, OriginsLongitudeNLatitude[0], OriginsLongitudeNLatitude[1]);
		//		store.Distance = distance;
		//		getSomeStoresWithDistance.Add(store);
		//	}

		//	var getSomeStoresOrderByDistance = getSomeStoresWithDistance.Where(x => x.Distance != -1).OrderBy(x => x.Distance).Skip((pageNum - 1) * storeNum).Take(storeNum).ToList();
		//	return getSomeStoresOrderByDistance;
		//}


		//private async Task<List<double>> getOriginsLongitudeNLatitude(string origin)
		//{
		//	var apiKey = await _context.Apis.Where(x => x.Id == 1).Select(x => x.Apikey).FirstOrDefaultAsync();
		//	var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={origin}&key={apiKey}";
		//	using var client = new HttpClient();
		//	var response = await client.GetAsync(url);
		//	var content = await response.Content.ReadAsStringAsync();
		//	dynamic result = JsonConvert.DeserializeObject(content);

		//	var OriginsLongitude = Convert.ToDouble(result.results[0].geometry.location.lng);
		//	var OriginsLatitude = Convert.ToDouble(result.results[0].geometry.location.lat);
		//	var OriginsLongitudeNLatitude = new List<double>() { OriginsLongitude,OriginsLatitude };

		//	return OriginsLongitudeNLatitude;
		//}

		//private async Task<double> getDistance(double storeLng, double storeLat, double originLng, double originLat)
		//{
		//	double R = 6371; // 地球平均半徑，單位為公里
		//	double dLat = Math.Abs(storeLat - originLat) * Math.PI / 180;
		//	double dLon = Math.Abs(storeLng - originLng) * Math.PI / 180;
		//	double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(originLat * Math.PI / 180) * Math.Cos(storeLat * Math.PI / 180) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
		//	double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
		//	double distance_km = R * c;



		//	//double dLat = Math.Abs(storeLat - originLat);
		//	//double dLon = Math.Abs(storeLng - originLng);

		//	//var distance_km = Math.Sqrt(Math.Pow(dLon, 2) + Math.Pow(dLat, 2)) * 110.9362;


		//	return distance_km;

		//}












		////1列出所有商店
		//// GET: api/Stores
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<StoreDTO>>> GetStores()
		//{
		//	var getAllStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Select(x => new StoreDTO
		//	{
		//		Id = x.Id,
		//		StorePrincipalId = x.StorePrincipalId,
		//		StoreName = x.StoreName,
		//		Address = x.Address,
		//		ContactNumber = x.ContactNumber,
		//		Photo = x.Photo,


		//		CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)

		//	}).ToListAsync();
		//	return getAllStores;
		//}
		////1.01列出所有商店並且分頁顯示

		//[HttpGet("getSomeStores")]
		//public async Task<ActionResult<IEnumerable<StoreDTO>>> GetSomeStores(int storeNum, int pageNum)
		//{
		//	var getSomeStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Select(x => new StoreDTO
		//	{
		//		Id = x.Id,
		//		StorePrincipalId = x.StorePrincipalId,
		//		StoreName = x.StoreName,
		//		Address = x.Address,
		//		ContactNumber = x.ContactNumber,
		//		Photo = x.Photo,


		//		CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)

		//	}).Skip((pageNum - 1) * storeNum).Take(storeNum).ToListAsync();
		//	return getSomeStores;
		//}


		////1.02列出所有商店並且按照距離分頁顯示
		////
		//[HttpGet("getSomeStoresIfIMAt/{origin}")]
		//public async Task<ActionResult<IEnumerable<Store1DTO>>> GetSomeStoresIfIMAt(int pageNum, int storeNum, string origin)
		//{
		//	var getSomeStores = await _context.Stores
		//		.Include(s => s.StoresCategoriesLists)
		//		.ThenInclude(x => x.Category)
		//		.Select(x => new Store1DTO
		//		{
		//			Id = x.Id,
		//			StorePrincipalId = x.StorePrincipalId,
		//			StoreName = x.StoreName,
		//			Address = x.Address,
		//			ContactNumber = x.ContactNumber,
		//			Photo = x.Photo,
		//			CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
		//		})
		//		.ToListAsync();

		//	var getSomeStoresWithDistance = new List<Store1DTO>();
		//	foreach (var store in getSomeStores)
		//	{
		//		var distance = await getDistance(store.Address, origin);
		//		store.Distance = distance;
		//		getSomeStoresWithDistance.Add(store);
		//	}

		//	var getSomeStoresOrderByDistance = getSomeStoresWithDistance.Where(x => x.Distance!=-1).OrderBy(x => x.Distance).Skip((pageNum - 1) * storeNum).Take(storeNum).ToList();
		//	return getSomeStoresOrderByDistance;
		//}

		//private async Task<int> getDistance(string storeAddress, string origin)
		//{
		//	var Distance = -1;
		//	var apiKey =await _context.Apis.Where(x => x.Id == 1).Select(x => x.Apikey).FirstOrDefaultAsync();

		//	var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={storeAddress}&key={apiKey}&mode=walking";
		//	using var client = new HttpClient();
		//	var response = await client.GetAsync(url);
		//	var content = await response.Content.ReadAsStringAsync();
		//	if (content == null)
		//	{
		//		return Distance;
		//	}

		//	dynamic result = JsonConvert.DeserializeObject(content);
		//	if (result == null || result.rows == null || result.rows.Count == 0 || result.rows[0].elements == null || result.rows[0].elements.Count == 0 || result.rows[0].elements[0].distance == null)
		//	{
		//		Distance = -1;
		//	}
		//	else
		//	{
		//		Distance = result.rows[0].elements[0].distance.value;
		//	}

		//	return Distance;
		//}


		//1.1列出所有類別
		// GET: api/StoreCategories
		[HttpGet("getStoreCategories")]
		public async Task<ActionResult<IEnumerable<StoreCategory>>> GetStoreCategories()
		{
			return await _context.StoreCategories.ToListAsync();
		}

		//整合全部條件顯示店家


		//      [HttpGet("getSomeStoresIfIMAt/{origin}/{categoryId?}")]
		//      public async Task<ActionResult<IEnumerable<Store1DTO>>> GetSomeThisCategoryOfStoresIfIMAt(int pageNum, int storeNum, string origin, int? categoryId)
		//      {
		//          List<Store1DTO> getSomeStores;


		//          if (categoryId != null)
		//          {
		//              getSomeStores = await _context.Stores
		//                  .Include(s => s.StoresCategoriesLists)
		//                  .ThenInclude(x => x.Category)
		//                  .Where(x => x.StoresCategoriesLists.Any(scl => scl.CategoryId == categoryId))
		//                  .Select(x => new Store1DTO
		//                  {
		//                      Id = x.Id,
		//                      StorePrincipalId = x.StorePrincipalId,
		//                      StoreName = x.StoreName,
		//                      Address = x.Address,
		//                      ContactNumber = x.ContactNumber,
		//                      Photo = x.Photo,
		//                      CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
		//                  })
		//                  .ToListAsync();
		//          }
		//          else
		//          {
		//              getSomeStores = await _context.Stores
		//                  .Include(s => s.StoresCategoriesLists)
		//                  .ThenInclude(x => x.Category)
		//                  .Select(x => new Store1DTO
		//                  {
		//                      Id = x.Id,
		//                      StorePrincipalId = x.StorePrincipalId,
		//                      StoreName = x.StoreName,
		//                      Address = x.Address,
		//                      ContactNumber = x.ContactNumber,
		//                      Photo = x.Photo,
		//                      CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName),
		//                  })
		//                  .ToListAsync();
		//          }


		//	var getSomeStoresWithDistance = new List<Store1DTO>();
		//	foreach (var store in getSomeStores)
		//	{
		//		var distance = await getDistance(store.Address, origin);
		//		store.Distance = distance;
		//		getSomeStoresWithDistance.Add(store);
		//	}

		//	var getSomeStoresOrderByDistance = getSomeStoresWithDistance.Where(x => x.Distance != -1).OrderBy(x => x.Distance).Skip((pageNum - 1) * storeNum).Take(storeNum).ToList();
		//	return getSomeStoresOrderByDistance;
		//}

		//      private async Task<int> getDistance(string storeAddress, string origin)
		//      {
		//          var Distance = -1;
		//          var apiKey = await _context.Apis.Where(x => x.Id == 1).Select(x => x.Apikey).FirstOrDefaultAsync();

		//          var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={storeAddress}&key={apiKey}&mode=walking";
		//          using var client = new HttpClient();
		//          var response = await client.GetAsync(url);
		//          var content = await response.Content.ReadAsStringAsync();
		//          if (content == null)
		//          {
		//              return Distance;
		//          }

		//          dynamic result = JsonConvert.DeserializeObject(content);
		//          if (result == null || result.rows == null || result.rows.Count == 0 || result.rows[0].elements == null || result.rows[0].elements.Count == 0 || result.rows[0].elements[0].distance == null)
		//          {
		//              Distance = -1;
		//          }
		//          else
		//          {
		//              Distance = result.rows[0].elements[0].distance.value;
		//          }

		//          return Distance;
		//      }








		




		//      //2依類別選出商店，點選類別傳入類別ID顯示商店

		//      [HttpGet("getStoresByCategoryId/{categoryId?}")]
		//public async Task<ActionResult<IEnumerable<StoreDTO>>> GetStores(int? categoryId)
		//{
		//	if (categoryId != null)
		//	{
		//		var getStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Where(x => x.StoresCategoriesLists.Any(scl => scl.CategoryId == categoryId)).Select(x => new StoreDTO
		//		{
		//			Id = x.Id,
		//			StorePrincipalId = x.StorePrincipalId,
		//			StoreName = x.StoreName,
		//			Address = x.Address,
		//			ContactNumber = x.ContactNumber,
		//			Photo = x.Photo,

		//			CategoryName = x.StoresCategoriesLists.Select(scl => scl.Category.CategoryName)
		//		})
		//		  .ToListAsync();

		//		return getStores;
		//	}
		//	else
		//	{
		//		var getAllStores = await _context.Stores.Include(s => s.StoresCategoriesLists).ThenInclude(x => x.Category).Select(x => new StoreDTO
		//		{
		//			Id = x.Id,
		//			StorePrincipalId = x.StorePrincipalId,
		//			StoreName = x.StoreName,
		//			Address = x.Address,
		//			ContactNumber = x.ContactNumber,
		//			Photo = x.Photo,


		//			CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)

		//		}).ToListAsync();
		//		return getAllStores;
		//	}
		//}



		////2.1依搜尋字串選出商店名稱、商店類別名稱、商店的商品名稱與搜尋字串有關的商店，如果商品狀態為FALSE則不列出
		//[HttpGet("searchString")]
		//public async Task<ActionResult<IEnumerable<StoreDTO>>> SearchStore(string? searchString)
		//{

		//		var searchStores = await _context.Stores.Include(x => x.StoresCategoriesLists).ThenInclude(x => x.Category).Include(x => x.Products).
		//		   Where(s => s.StoreName.Contains(searchString)
		//		   || s.StoresCategoriesLists.Any(scl => scl.Category.CategoryName.Contains(searchString))
		//		   || s.Products.Any(p => p.ProductName.Contains(searchString) && s.Products.Any(p => p.Status == true))).Select(x => new StoreDTO
		//		   {
		//			   Id = x.Id,
		//			   StorePrincipalId = x.StorePrincipalId,
		//			   StoreName = x.StoreName,
		//			   Address = x.Address,
		//			   ContactNumber = x.ContactNumber,
		//			   Photo = x.Photo,

		//			   CategoryName = x.StoresCategoriesLists.Select(s => s.Category.CategoryName)
		//		   }).ToListAsync();
		//	return searchStores;
		//}



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

		////6商店內部資訊修改
		//[HttpPut("{id}")]
		//public async Task<string> PutStore(int id, Store store)
		//{
		
		//	if (id != store.Id)
		//	{
		//		return "錯誤";
		//	}

		//	_context.Entry(store).State = EntityState.Modified;

		//	try
		//	{
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException ex)
		//	{

		//		if (!_context.Stores.Any(e => e.Id == id))
		//		{
		//			return "錯誤找不到此商店";
		//		}
		//		else
		//		{
		//			throw new Exception(ex.Message);
		//		}
		//	}

		//	return "修改成功";
		//}


		//6.1商店標籤新增


		//6.2商店標籤刪除



		//7商店內部商品新增



		//7.1商店內部商品修改

		//7.2商店內部商品刪除

	}
}















