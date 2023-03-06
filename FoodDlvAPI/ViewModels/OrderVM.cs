using System.ComponentModel.DataAnnotations;

namespace FoodDlvAPI.ViewModels
{
    public class OrderVM
    {
        [Display(Name = "訂單編號")]
        public long Id { get; set; }

        [Display(Name = "外送員編號")]
        public int DeliveryDriversId { get; set; }

        [Display(Name = "會員編號")]
        public int MemberId { get; set; }

        [Display(Name = "商家編號")]
        public int StoreId { get; set; }

        [Display(Name = "外送費")]
        public int DeliveryFee { get; set; }

        [Display(Name = "外送地址")]
        public string DeliveryAddress { get; set; }

        [Display(Name = "外送員評價")]
        public int? DriverRating { get; set; }

        [Display(Name = "商家評價")]
        public int? StoreRating { get; set; }

        [Display(Name = "外送員評論")]
        public string DriverComment { get; set; }

        [Display(Name = "商家評論")]
        public string StoreComment { get; set; }

        [Display(Name = "里程數")]
        public decimal Milage { get; set; }
        public List<OrderDetailDTO> Details { get; set; }
    }
}
