using FoodDlvAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 提供購物車使用的商品類別與客製化資訊
    /// </summary>
    public class CartDetailDTO
    {
        //Fields                
        private int _Qty;

        //Properties
        public int Id { get; set; }
        public int IdentifyNum { get; set; }
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ItemId { get; set; } = 0;
        public string? ItemName { get; set; }
        public int ItemPrice { get; set; } = 0;
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value > 0 ? value : throw new Exception("Qty需為正數"); }
        }
        public long CartId { get; set; }
        public int SubTotal { get; set; }
            
        //Constructors
        public CartDetailDTO (int id, int identifyNum, long productId, int itemId, int qty, long cartId)
        {
            Id = id;
            IdentifyNum = identifyNum;
            ProductId = productId;
            ItemId = itemId;
            Qty = qty;
            CartId = cartId;                
        }

        public CartDetailDTO(int identifyNum, long productId, int itemId, int qty, long cartId)
        {
            IdentifyNum = identifyNum;
            ProductId = productId;
            ItemId = itemId;
            Qty = qty;
            CartId = cartId;
        }                
    }

    public static partial class CartDetailExts
    {
        public static CartDetailDTO ToCartDetailDTO(this CartDetail source)
        {
            var cartDetailDTO = new CartDetailDTO
                (
                    source.Id,
                    source.IdentifyNum,
                    source.ProductId,
                    source.ItemId,
                    source.Qty,
                    source.CartId
                );           
            return cartDetailDTO;
        }

        public static CartDetail ToCartDetailEF(this CartDetailDTO source)
        {
            var cartDetailEF = new CartDetail
            {
               Id= source.Id,
               IdentifyNum= source.IdentifyNum,
               ProductId= source.ProductId,
               ItemId= source.ItemId,
               Qty= source.Qty,
               CartId= source.CartId
            };
            return cartDetailEF;
        }
    }
}
