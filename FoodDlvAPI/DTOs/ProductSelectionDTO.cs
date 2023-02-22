using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    public class ProductSelectionDTO
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public int StoreId { get; set; }        
        public string? ProductPhoto { get; set; }
        public string? Content { get; set; }
        public bool? Status { get; set; }
        public int UnitPrice { get; set; }
        public virtual IEnumerable<ProductCustomizationItemDTO>? customizationItem { get; set; }
    }
}
