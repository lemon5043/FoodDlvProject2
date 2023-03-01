using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 供購物車使用的商品類別,僅包含必要資訊
    /// </summary>
    public class CartProductDTO
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public List<ProductCustomizationItemDTO> CustomizationItems { get; set; }
    }

    public static partial class ProdyctExts
    {
        public static CartProductDTO ToCartProductDTO(this Product source)
        {
            CartProductDTO cartProductDTO = new CartProductDTO()
            {
                Id = source.Id,
                ProductName = source.ProductName,
                Price = source.UnitPrice,
                CustomizationItems = source.ProductCustomizationItems
                    .Select(pci => new ProductCustomizationItemDTO(pci.Id, pci.ProuctId, pci.ItemName, pci.UnitPrice))
                    .ToList(),
            };
            return cartProductDTO;
        }
    }
}
