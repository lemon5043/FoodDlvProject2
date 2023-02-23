namespace FoodDlvAPI.DTOs
{
    public class CartCustomizationItemDTO
    {

        public int Id { get; set; }
        public int CustomizationItemId { get; set; }
        public long ProductId { get; set; }
        public int CartDetailId { get; set; }        
        public int Count { get; set; }
        public int IdentifyNum { get; set; }

        public CartCustomizationItemDTO(int customizationItemId, long productId, int cartDetailId, int count, int identifyNum)
        {            
            this.CustomizationItemId = customizationItemId;
            this.ProductId = productId;
            this.CartDetailId = cartDetailId;
            this.Count = count;
            this.IdentifyNum = identifyNum;
        }
    }
}
