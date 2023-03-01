using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class ProductEditVM
	{
        public long Id { get; set; }
        [Display(Name = "店家")]
        public int StoreId { get; set; }
        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }
        [Display(Name = "圖片")]
        public IFormFile? Photo { get; set; }
        [Display(Name = "商品內容")]
        public string? ProductContent { get; set; }
        [Display(Name = "狀態")]
        public bool? Status { get; set; }
        [Display(Name = "單價")]
        public int UnitPrice { get; set; }
    }
}
