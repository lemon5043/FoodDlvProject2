using FoodDlvProject2.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class OrderDetailVM
    {
        public long Id { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }

        [Display(Name = "商品編號")]
        public long ProductId { get; set; }

        [Display(Name = "商品名稱")]
        public string? ProductName { get; set; }

        [Display(Name = "商品單價")]
        public int UnitPrice { get; set; }

        [Display(Name = "商品數量")]
        public int Count { get; set; }

		//[Display(Name = "客製化項目")]
		//public string CustomizationItems { get; set; }

		//[Display(Name = "客製化數量")]
		//public int CustomizationItemCount { get; set; }

		[Display(Name = "單品總價")]
        public int SubTotal { get; set; }        
    }

    public static partial class OrderDetailDtoExts
    {
        public static OrderDetailVM ToOrderDetailVM(this OrderDetailDto source)
        {
            return new OrderDetailVM
            {
                Id = source.Id,
                OrderId = source.OrderId,
                ProductId = source.ProductId,
                ProductName = source.ProductName,
                UnitPrice = source.UnitPrice,
                Count = source.Count,
                SubTotal = source.SubTotal,
            };
        }
    }
}
