using FoodDlvAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 提供購物車使用的商品與客製化資訊
    /// </summary>
    public class CartDetailDTO
    {        
        //Properties
        public int Id { get; set; }
        public int IdentifyNum { get; set; }
        public long ProductId { get; set; }
        public string? ProductName { get; set; }        
        public int ItemId { get; set; }
        public List<int> ItemsId { get; set; }
        public string? ItemName { get; set; }       
        public int Qty { get; set; }      
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

        //public CartDetailDTO(int identifyNum, long productId, int itemId, int qty, long cartId)
        //{
        //    IdentifyNum = identifyNum;
        //    ProductId = productId;
        //    ItemId = itemId;
        //    Qty = qty;
        //    CartId = cartId;
        //}        
        
        public CartDetailDTO() { }
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
               Id = source.Id,
               IdentifyNum = source.IdentifyNum,
               ProductId = source.ProductId,
               ItemId = source.ItemId,
               Qty = source.Qty,
               CartId = source.CartId
            };
            return cartDetailEF;
        }
    }
}
