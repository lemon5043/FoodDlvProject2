namespace FoodDlvAPI.DTOs
{
    public class ProductCustomizationItemDTO
    {
        public int Id { get; set; }
        public long ProuctId { get; set; }
        public string? ItemName { get; set; }
        public int CustomizationItemPrice { get; set; }
    }
}
