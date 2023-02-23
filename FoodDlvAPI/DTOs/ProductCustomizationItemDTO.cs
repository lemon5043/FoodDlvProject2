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
            this.Id = id;
            this.ProuctId = productId;
            this.ItemName = itemName;
            this.UnitPrice = unitPrice;
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
