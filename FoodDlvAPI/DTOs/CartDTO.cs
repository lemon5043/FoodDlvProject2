using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    public class CartDTO
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }
        
        public CartDTO(int memberId, int storeId)
        {
            this.MemberId = memberId;
            this.StoreId = storeId;
        }
        public CartDTO(long id, int memberId, int storeId)
        {
            this.Id = id;
            this.MemberId = memberId;
            this.StoreId = storeId;
        }
    }
    public static class CartExts
    {
        public static CartDTO ToCartDTO(this Cart source)
        {            
            var toCartDTO = new CartDTO
            (         
                source.Id,
                source.MemberId,
                source.StoreId               
            );
            return toCartDTO;
        }

        public static Cart ToCartEntity(this CartDTO source)
        {
            var toCartEntity = new Cart
            {               
                MemberId = source.MemberId,
                StoreId = source.StoreId,                
            };
            return toCartEntity;
        }
    }
}
