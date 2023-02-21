using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    
    public class OrderProductDetailDto
    {		
		public long Id { get; set; }
				
		public int StoreId { get; set; }
				
		public string StoreName { get; set; }

		public string ProductName { get; set; }

		public int UnitPrice { get; set; }

		public string Photo { get; set; }

		public string ProductContent { get; set; }
	}

    public static partial class ProductExts
    {
        public static OrderProductDetailDto ToOrderProductDto(this Product source)
        {
            return new OrderProductDetailDto
			{
				Id = source.Id, 
				StoreId = source.StoreId,
				StoreName = source.Store.StoreName,
				ProductName = source.ProductName,
				UnitPrice = source.UnitPrice,
				Photo = source.Photo,
				ProductContent = source.ProductContent,				
			};
        }
    }

}
