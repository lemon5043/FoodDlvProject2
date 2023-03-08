using FoodDlvAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 提供購物車使用的購物車資訊載體
    /// </summary>
    public class CartDTO
    {              
        //Properties
        public long Id { get; set; }
        public int MemberId { get; set; }
        public string? MemberName { get; set; }
        public int StoreId { get; set; }
        public string? StoreName { get; set; }
        public int DetailQty { get; set; } = 0;
        public int Total { get; set; } = 0;
        public List<CartDetailDTO> Details { get; set; }        

        //Constructors
        public CartDTO(long id, int memberId, int storeId, List<CartDetailDTO> details)
        {
            Id = id;
            MemberId = memberId;
            StoreId = storeId;
            Details = details;
        }
        public CartDTO() { }
                  
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
                CartDetails = source.Details.Select(cd => cd.ToCartDetailEF()).ToList()
            };
            return cartEF;            
        }
    }
}
