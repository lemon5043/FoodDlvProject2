using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    public class CartDTO
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }
       
        public virtual ICollection<CartDetail> CartDetails { get; set; }        

        public CartDTO(long id, int memberId, int storeId, 
                        ICollection<CartDetail> CartDetails) 
        {
            this.Id = id;
            this.MemberId = memberId;
            this.StoreId = storeId;
            this.CartDetails = CartDetails;
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
                source.StoreId,
                source.CartDetails
            );
            return toCartDTO;
        }

        public static Cart ToCartEntity(this CartDTO source)
        {
            var toCartEntity = new Cart
            {
                Id = source.Id,
                MemberId = source.MemberId,
                StoreId = source.StoreId,
                CartDetails = source.CartDetails
            };
            return toCartEntity;
        }
    }
}
