using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryViolationTypesVM
    {
        public int Id { get; set; }

        public string ViolationContent { get; set; }

        public string Content { get; set; }
    }

    public static class DeliveryViolationTypesVMExts
    {
        public static DeliveryViolationTypesVM ToDeliveryViolationTypesVM(this DeliveryViolationTypesDTO source)
        {
            return new DeliveryViolationTypesVM
            {
                Id = source.Id,
                ViolationContent = source.ViolationContent,
                Content = source.Content,
            };
        }
    }
}
