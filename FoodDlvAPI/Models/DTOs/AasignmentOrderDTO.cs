using FoodDlvAPI.Models.ViewModels;

namespace FoodDlvAPI.Models.DTOs
{
    public class AasignmentOrderDTO
    {
        public long OrderId { get; set; }
        public string StoreAddress { get; set; }
        public string StoreName { get; set; }
        public string DeliveryAddress { get;  set; }
        public int DriverId { get; set; }
        //todo public OrderDetailsDTO OrderDetails { get;  set; }
    }

}