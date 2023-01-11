using System;
using System.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class OrderEntity
    {
        public long Id { get; set; }
        public DeliveryDriverDto DeliveryDriver { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }

        public DateTime OrderTime { get; set; }

        public List<OrderDetailEntity> Items { get; set; }

        public int DeliveryFee { get; set; }

        public int Total => Items.Sum(x => x.SubTotal) + DeliveryFee;

        public string DeliveryAddress { get; set; }        
        public decimal Milage { get; set; }

        public OrderEntity(long id, int memberId, int storeId)
        {

        }
    }

    public static partial class OrderExts
    {
        public static OrderEntity ToEntity(this OrderEntity source)
        {   
          
            return new OrderEntity
            {
                Id= source.Id,
                DeliveryDriver = source.DeliveryDriver.ToEntity(),
                DeliveryAddress = source.DeliveryAddress,                
                Milage = source.Milage,
            };
        }
    }
}
