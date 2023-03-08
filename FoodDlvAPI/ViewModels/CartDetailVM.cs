using FoodDlvAPI.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvAPI.ViewModels
{
    public class CartDetailVM
    {
        [Display(Name = "商品識別編號")]
        public int IdentifyNum { get; set; }

        [Display(Name = "商品編號")]
        public long ProductId { get; set; }

        [Display(Name = "商品名稱")]
        public string? ProductName { get; set; }

        [Display(Name = "客製化選項編號")]
        public List<int?>? ItemsId { get; set; }

        [Display(Name = "客製化內容")]
        public string? ItemName { get; set; }

        [Display(Name = "商品數量")]
        public int Qty { get; set; }

        [Display(Name = "購物車編號")]
        public long CartId { get; set; }

        [Display(Name = "單項商品小計")]
        public int SubTotal { get; set; }
    }

    public static partial class CartDetailVMExts
    {
        public static CartDetailVM ToCartDetailVM(this CartDetailDTO source)
        {
            var cartDetailVM = new CartDetailVM
            {
                IdentifyNum = source.IdentifyNum,
                ProductId = source.ProductId,
                ProductName = source.ProductName,
                ItemsId = source.ItemsId,
                ItemName = source.ItemName,
                Qty = source.Qty,
                CartId = source.CartId,
                SubTotal = source.SubTotal,
            };
            return cartDetailVM;
        }
    }
}
