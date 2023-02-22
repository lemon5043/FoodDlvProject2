using FoodDlvAPI.Models;

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

        public CartDTO(long id, int memberId, 
                        int storeId, long productId, int count) 
        {
            this.Id = id;
            this.MemberId = memberId;
            this.StoreId = storeId;
            this.ProductId = productId;
            this.Count = count;
        }
    }

    public static class CartExts
    {
        public static CartDTO ToDTO(this Cart source)
        {
            var toDTO = new CartDTO
            (
                source.Id,
                source.MemberId,
                source.StoreId,
                source.ProductId,
                source.Count
            );

            return toDTO;
        }
    }
}
