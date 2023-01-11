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
        public int DeliveryFee { get; set; }
        public string DeliveryAddress { get; set; }
        public int? DriverRating { get; set; }
        public int? StoreRating { get; set; }
        public string DriverComment { get; set; }
        public string StoreComment { get; set; }
        public decimal Milage { get; set; }

        
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
                DriverRating = source.DriverRating,
                StoreRating = source.StoreRating,
                DriverComment = source.DriverComment,
                StoreComment = source.StoreComment,
                Milage = source.Milage,
            };
        }
    }
}
