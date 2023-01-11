using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class OrderDetailEntity
    {       
        public long Id { get; set; }
        
        public long OrderId { get; set; }
             
        public OrderProductEntity Product { get; set; }
              
        public int Count { get; set; }
             
        public int SubTotal => Product.UnitPrice * Count;

        public OrderDetailEntity(long id, long orderId, OrderProductEntity product, int count)
        {
            Id = id;
            OrderId = orderId;
            Product = product;
            Count = count;
        }
    }

    public static partial class OrderDetailExts
    {
        public static OrderDetailEntity ToEntity(this OrderDetail source) 
        {
            return new OrderDetailEntity(source.Id, source.OrderId ,source.Product.ToOrderProductEntity(), source.Count);
        }
    }
}
