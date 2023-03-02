using FoodDlvAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvAPI.DTOs
{
    public class CartDTO
    {
        //Fields
        private List<CartDetailDTO> Details;

        //Properties
        public long Id { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }
        //private List<CartDetailDTO> Details { get; set; }
        //public int Total => Details == null || Details.Count == 0 ? 0 : Details.Sum(d => d.SubTotal);

        //Constructors
        public CartDTO(long id, int memberId, int storeId, List<CartDetailDTO> details)
        {
            Id = id;
            MemberId = memberId;
            StoreId = storeId;
            Details = details;
        } 
        public CartDTO(int memberId, int storeId, List<CartDetailDTO> details)
        {
            MemberId = memberId;
            StoreId = storeId;
            Details = details;
        }

        //Methods
        public IEnumerable<CartDetailDTO> GetDetails()
        {
            return Details;
        }
    }
    public static class CartExts
    {
        public static CartDTO ToCartDTO(this Cart source)
        {
            var cartDTO = new CartDTO
            (
                source.Id,
                source.MemberId,
                source.StoreId,
                source.CartDetails.Select(cd => cd.ToCartDetailDTO()).ToList()
            );
            return cartDTO;            
        }

        public static Cart ToCartEF(this CartDTO source)
        {
            var cartEF = new Cart
            {
                Id = source.Id,
                MemberId = source.MemberId,
                StoreId = source.StoreId,
                CartDetails = source.GetDetails().Select(cd => cd.ToCartDetailEF()).ToList()
            };
            return cartEF;            
        }
    }
}
