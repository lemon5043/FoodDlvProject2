using FoodDlvProject2.EFModels;
using System;
using System.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class OrderDto
    {
        public long Id { get; set; }
        //public DeliveryDriverDto DeliveryDriver { get; set; }
        public string MemberName { get; set; }
        public string StoreName { get; set; }

        //public ICollection<OrderSchedule> OrderSchedules { get; set; }
        public DateTime OrderTime { get; set; }
        public List<OrderDetailDto> Items { get; set; }

        public int DeliveryFee { get; set; }

        public int Total { get; set; }

        //public string DeliveryAddress { get; set; }        
        public decimal Milage { get; set; }
    }

    public static partial class OrderExts
    {
        public static OrderDto ToOrderDto(this Order source)
        {            
            return new OrderDto
            { 
                Id = source.Id,
                MemberName = source.Member.LastName + source.Member.FirstName,
                StoreName = source.Store.StoreName,
                OrderTime = source.OrderSchedules.SingleOrDefault().MarkTime,
                //OrderSchedules = source.OrderSchedules,
                
                DeliveryFee = source.DeliveryFee,                            

            };
        }
    }
}
