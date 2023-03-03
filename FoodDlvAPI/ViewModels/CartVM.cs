
using FoodDlvAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvAPI.ViewModels
{
    public class CartVM
    {   
        /// <summary>
        /// View可能需要的資料
        /// </summary>
        [Display(Name = "會員編號")]
        public int MemberId { get; set; }

        [Display(Name = "店家編號")]
        public int StoreId { get; set; }                

        [Display(Name = "產品名稱")]
        public string ProductName { get; set; }

        [Display(Name = "項目總價")]        
        public int Price { get; set; }

        [Display(Name = "數量")]
        public int Qty { get; set; }

        [Display(Name = "客製化")]
        public string Items { get; set; }


        /// <summary>
        /// 傳回屬性, 
        /// </summary>
        public int ProductId { get; set; }
        public List<int> ItemId { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int RD_MemberId { get; set; }

        [Required]
        public int RD_StoreId { get; set; }

        [Required]
        public long RD_ProductId { get; set; }        
        public List<int>? RD_Item { get; set; }                
        public int RD_Qty { get; set; } = 1;
    }

    public static partial class CartVMExts
    {
        public static CartVM ToCartVM(this CartDTO source)
        {
            var cartVM = new CartVM
            {
                MemberId = source.MemberId,
                StoreId = source.StoreId,
                ProductId = source,
                ProductName = source,
                Price = source,
                Qty = source,
                Items = source,
            };
        }
        public static CartDTO ToCartDTO(this CartVM source)
        {
            

            var cartDTO = new CartDTO
            (
                source.RD_MemberId,
                source.RD_StoreId,
                details
            );
            return cartDTO;
        }
    }
}
