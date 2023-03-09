namespace FoodDlvAPI.Models.DTOs
{
    public class DeliveryEndDTO
    {
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public decimal Milage { get; set; }
    }
    public static class DeliveryEndDTOExts
    {
        public static Order ToEFModel(this DeliveryEndDTO dto)
             => new Order
             {
                 Id = dto.OrderId,
                 DeliveryDriversId = dto.DriverId,
                 Milage = dto.Milage,
             };
    }
}