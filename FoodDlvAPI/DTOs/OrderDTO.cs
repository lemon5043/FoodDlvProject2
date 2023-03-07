using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public int DeliveryDriversId { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }
        public int DeliveryFee { get; set; }
        public string DeliveryAddress { get; set; }
        public int? DriverRating { get; set; }
        public int? StoreRating { get; set; }
        public string DriverComment { get; set; }
        public string StoreComment { get; set; }
        public decimal Milage { get; set; }
        public CartDTO Cart { get; set; }
        public List<OrderDetailDTO> Details { get; set; }
    }

    public static partial class OrderExts
    {
        public static OrderDTO ToOrderDTO(this Order source)
        {
            var orderDTO = new OrderDTO() { };
            return orderDTO;
        }
    }
}
