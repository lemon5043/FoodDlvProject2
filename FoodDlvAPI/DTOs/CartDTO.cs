namespace FoodDlvAPI.DTOs
{
    public class CartDTO
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public ProductCustomizationItemDTO? CustomizationItem { get; set; }
    }
}
