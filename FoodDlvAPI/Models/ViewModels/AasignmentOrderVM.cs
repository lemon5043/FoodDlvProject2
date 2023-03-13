using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.ViewModels
{
    public class AasignmentOrderVM
    {
        public long OrderId { get; set; }
        public int MembreId { get; set; }
        public string StoreAddress { get; set; }
        public string StoreName { get; set; }
    }

    public static class AasignmentOrderVMExts
    {
        public static AasignmentOrderVM ToAasignmentOrderVM(this AasignmentOrderDTO dTO)
            => new AasignmentOrderVM
            {
                OrderId = dTO.OrderId,
                MembreId = dTO.MemberId,
                StoreAddress = dTO.StoreAddress,
                StoreName = dTO.StoreName,
            };
    }
}