using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class OrderDetailVM
    {
        public long Id { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }

        [Display(Name = "商品編號")]
        public long ProductId { get; set; }

        [Display(Name = "商品名稱")]
        public string? ProductName { get; set; }

        [Display(Name = "商品單價")]
        public int UnitPrice { get; set; }

        [Display(Name = "商品數量")]
        public int Count { get; set; }

        [Display(Name = "單品總價")]
        public int SubTotal => UnitPrice * Count;        
    }
}
