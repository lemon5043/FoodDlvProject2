using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    public class ProductCustomizationItemDTO
    {
        public int Id { get; set; }
        public long ProuctId { get; set; }
        public string? ItemName { get; set; }
        public int UnitPrice { get; set; }

        public ProductCustomizationItemDTO(int id, long productId, string itemName, int unitPrice)
        {
            Id = id;
            ProuctId = productId;
            ItemName = itemName;
            UnitPrice = unitPrice;
        }
    }

    public static partial class ProductCustomizationItemExts
    {
        public static ProductCustomizationItemDTO ToProductCustomizationItemDTO(this ProductCustomizationItem source)
        {
            var toProductCustomizationItemDTO = new ProductCustomizationItemDTO
                (
                    source.Id,
                    source.ProuctId,
                    source.ItemName,
                    source.UnitPrice
                );
            return toProductCustomizationItemDTO;
        }
    }
}
