using System.ComponentModel.DataAnnotations;

namespace FoodDlvAPI.ViewModels
{
    public class ProductSelectionVM
    {
        [Display(Name = "商品編號")]
        public long ProductId { get; set; }

        [Display(Name = "店家編號")]
        public int StoreId { get; set; }

        [Display(Name = "商品名稱")]
        public string? ProductName { get; set; }

        [Display(Name = "商品圖像")]
        public string? Photo { get; set; }

        [Display(Name = "商品內容")]
        public string? ProductContent { get; set; }

        [Display(Name = "商品狀態")]
        public bool? Status { get; set; }

        [Display(Name = "商品單價")]
        public int UnitPrice { get; set; }

        [Display(Name = "客製化編號")]
        public int Id { get; set; }

        [Display(Name = "客製化項目")]
        public string? ItemName { get; set; }

        [Display(Name = "客製化價格")]
        public int CustomizationItemPrice { get; set; }
    }
}
