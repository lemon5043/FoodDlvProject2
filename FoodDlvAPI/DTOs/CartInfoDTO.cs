namespace FoodDlvAPI.DTOs
{
    public class CartInfoDTO
    {
        public CartDTO CartDTO { get; set; }
        public IEnumerable<CartDetailDTO> CartDetailDTO { get; set; }
        public IEnumerable<CartCustomizationItemDTO> CartCustomizationItemDTO { get; set; }
    }
}
