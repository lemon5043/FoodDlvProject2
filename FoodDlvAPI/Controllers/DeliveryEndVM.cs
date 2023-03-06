namespace FoodDlvAPI.Controllers
{
    public class DeliveryEndVM
    {
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public decimal Milage { get; set; }
    }
    public static class DeliveryEndVMExts 
    {
        public static DeliveryEndDTO ToDeliveryEndDTO(this DeliveryEndVM deliveryEnd)
            => new DeliveryEndDTO
            {
                OrderId = deliveryEnd.OrderId,
                DriverId = deliveryEnd.DriverId,
                Milage = deliveryEnd.Milage,
            };

    }
}