
using FoodDlvAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvAPI.ViewModels
{
    public class CartVM
    {   
        //ViewInfo
        [Display(Name = "會員編號")]
        public int MemberId { get; set; }

        [Display(Name = "店家編號")]
        public int StoreId { get; set; }

        [Display(Name = "產品編號")]
        public long ProductId { get; set; }

        [Display(Name = "產品名稱")]
        public string ProductName { get; set; }

        [Display(Name = "產品單價")]        
        public int UnitPrice { get; set; }

        [Display(Name = "客製化")]
        public IEnumerable<ProductCustomizationItemVM>? Item { get; set; }

        [Display(Name = "數量")]        
        public int Qty { get; set; }

        
        //RequestData
        public int RD_MemberId { get; set; }
        public int RD_StoreId { get; set; }
        public long RD_ProductId { get; set; }        
        public List<int>? RD_Item { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "數量必須為正整數")]
        public int RD_Qty { get; set; } = 1;
    }

    public static partial class CartVMExts
    {
        //public static CartDTO ToCartDTO(this CartVM source) 
        //{
        //    var detail = new CartDetailDTO(source.ProductId, source.RD_Item, source.Qty);
        //    var cartDTO = new CartDTO
        //    (             
        //        source.RD_MemberId,
        //        source.RD_StoreId,
        //        detail
        //    );
        //}
    }
}
