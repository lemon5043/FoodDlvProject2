using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    public class CartCustomizationItemDTO
    {

        public int Id { get; set; }
        public int CustomizationItemId { get; set; }
        public long ProductId { get; set; }
        public int CartDetailId { get; set; }        
        public int Count { get; set; }
        public int IdentifyNum { get; set; }

        public CartCustomizationItemDTO(int customizationItemId, long productId, int cartDetailId, int count, int identifyNum)
        {                       
            this.CustomizationItemId = customizationItemId;
            this.ProductId = productId;
            this.CartDetailId = cartDetailId;
            this.Count = count;
            this.IdentifyNum = identifyNum;
        }
    }

    public static partial class CartCustomizationItemExts
    {
        public static CartCustomizationItemDTO ToCartCustomizationItemDTO(this CartCustomizationItem source)
        {
            var toCartCustomizationItemDTO = new CartCustomizationItemDTO
            (                
                source.CustomizationItemId,
                source.ProductId,
                source.CartDetailId,
                source.Count,
                source.IdentifyNum
            );
            return toCartCustomizationItemDTO;
        }
        public static CartCustomizationItem ToCartCustomizationItemEntity(this CartCustomizationItemDTO source) 
        { 
            var toCartCustomizationItemEntity = new CartCustomizationItem
            {                
                CustomizationItemId = source.CustomizationItemId,
                ProductId = source.ProductId,
                CartDetailId = source.CartDetailId,
                Count = source.Count,
                IdentifyNum = source.IdentifyNum
            };
            return toCartCustomizationItemEntity;
        }
    }
}
