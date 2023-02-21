namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 提供購物車使用的商品類別與客製化資訊
    /// </summary>
    public class CartProductDTO
    {           
        public long ProductId { get; set; }       
        public int Price { get; set; }
        public ProductCustomizationItemDTO CustomizationItem { get; set; }
    }
}
