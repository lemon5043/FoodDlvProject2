using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class OrderDetailDto
    {       
        public long Id { get; set; }
        
        public long OrderId { get; set; }

		public long ProductId { get; set; }

        public string ProductName { get; set; }

		public int UnitPrice { get; set; }
              
        public int Count { get; set; }
             
        public int SubTotal { get; set; }
               
    }

    public static partial class OrderDetailExts
    {
        public static OrderDetailDto ToOrderDetailDto(this OrderDetail source, string productName, int subTotal) 
        {
            return new OrderDetailDto 
            { 
                Id = source.Id,
                OrderId = source.OrderId,
                ProductId = source.ProductId,
                ProductName = productName,
                UnitPrice = source.UnitPrice, 
                Count = source.Count,
                SubTotal = subTotal,
            };
        }
    }
}
