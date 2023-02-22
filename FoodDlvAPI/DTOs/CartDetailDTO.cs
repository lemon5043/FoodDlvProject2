using FoodDlvAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 提供購物車使用的商品類別與客製化資訊
    /// </summary>
    public class CartDetailDTO
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public long CartId { get; set; }
        public virtual ICollection<CartCustomizationItem> CustomizationItem { get; set; }
    
        public CartDetailDTO(int id, long productId, int qty, int price, long cartId, ICollection<CartCustomizationItem> customizationItem)
        {
            this.Id = id;
            this.ProductId = productId;
            this.Qty = qty;
            this.Price = price;
            this.CartId = cartId;
            this.CustomizationItem = customizationItem;
        }
    }

    public static partial class CartDetailExts
    {
        public static CartDetailDTO ToCartDetailDTO(this CartDetail source, int unitPrice, ICollection<CartCustomizationItem> customizationItem)
        {
            var toCartDetail = new CartDetailDTO
            (
                source.Id,
                source.ProductId,
                source.Qty,
                unitPrice,
                source.CartId,
                customizationItem
            );
            return toCartDetail;
        }
    }
}
