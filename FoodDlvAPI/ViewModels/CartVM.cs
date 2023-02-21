using FoodDlvAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvAPI.ViewModels
{
    public class CartVM
    {       
        [Display(Name = "會員編號")]
        public int MemberId { get; set; }
        [Display(Name = "店家編號")]
        public int StoreId { get; set; }
        [Display(Name = "產品編號")]
        public long ProductId { get; set; }
        [Display(Name = "產品單價")]
        public int UnitPrice { get; set; }
        [Display(Name = "數量")]
        public int Qty { get; set; }
        [Display(Name = "客製化")]
        public ProductCustomizationItemVM customizationItem { get; set; }
    }
}
