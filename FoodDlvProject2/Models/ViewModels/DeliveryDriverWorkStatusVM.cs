using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.ViewModels
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
