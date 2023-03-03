using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 供購物車使用的商品類別,僅包含必要資訊
    /// </summary>
    public class CartProductDTO
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int Qty { get; set; }
        public List<ProductCustomizationItemDTO>? Items { get; set; }        
    }

    public static partial class ProdyctExts
    {
        public static CartProductDTO ToCartProductDTO(this Product source)
        {
            CartProductDTO cartProductDTO = new CartProductDTO()
            {
                ProductId = source.Id,
                ProductName = source.ProductName,
                ProductPrice = source.UnitPrice,                
                Items = source.ProductCustomizationItems
                    .Select(pci => pci.ToProductCustomizationItemDTO())
                    .ToList(),
            };
            return cartProductDTO;
        }
    }
}
