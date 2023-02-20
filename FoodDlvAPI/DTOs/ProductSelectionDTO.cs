using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    public class ProductSelectionDTO
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public int StoreId { get; set; }        
        public byte[]? ProductPhoto { get; set; }
        public string? ProductContent { get; set; }
        public bool? ProductStatus { get; set; }
        public int UnitPrice { get; set; }
        public IEnumerable<ProductCustomizationItemDTO>? customizationItem { get; set; }
    }
}
