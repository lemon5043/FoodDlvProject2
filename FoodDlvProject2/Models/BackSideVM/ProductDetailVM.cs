using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.BackSideVM
{
    public class ProductDetailVM
    {        
        public long Id { get; set; }

        [Display(Name = "商店編號")]
        public int StoreId { get; set; }

        [Display(Name = "商店名稱")]
        public string StoreName { get; set; }

        [Display(Name = "產品名稱")]
        public string ProductName { get; set; }

        [Display(Name = "產品圖像")]
        public byte[]? Photo { get; set; }

        [Display(Name = "產品敘述")]
        public string? ProductContent { get; set; }       
    }
}
