using FoodDlvAPI.EFModels;

namespace FoodDlvAPI.Models.ViewModels
{
    public class DeliveryDriverWorkStatusVM
    {
        public int Id { get; set; }

        public string Status { get; set; }
    }
    public static class WorkStatusExts
    {
        public static DeliveryDriverWorkStatusVM ToDeliveryDriverWorkStatusVM(this DeliveryDriverWorkStatusDTO source)
        {
            return new DeliveryDriverWorkStatusVM
            {
                Id = source.Id,
                Status = source.Status,
            };
        }
    }
}
