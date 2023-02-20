using FoodDlvProject2.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
	public class OrderProductVM
	{
		[Display(Name = "產品編號")]
		public long Id { get; set; }		

		[Display(Name = "店家名稱")]
		public string StoreName { get; set; }

		[Display(Name = "產品名稱")]
		public string ProductName { get; set; }

		[Display(Name = "產品單價")]
		public int UnitPrice { get; set; }

		[Display(Name = "產品圖片")]
		public string Photo { get; set; }

		[Display(Name = "產品說明")]
		public string ProductContent { get; set; }
	}

	public static partial class OrderProductDtoExts
	{
		public static OrderProductVM ToOrderProductVM(this OrderProductDetailDto source)
		{
			return new OrderProductVM
			{
				Id = source.Id,
				StoreName = source.StoreName,
				ProductName = source.ProductName,
				UnitPrice = source.UnitPrice,
				Photo = source.Photo,
				ProductContent = source.ProductContent,
			};
		}
	}
}
