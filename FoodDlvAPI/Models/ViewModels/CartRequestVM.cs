using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.ViewModels
{
    public class CartRequestVM
    {
        /// <summary>
        /// ResponseData
        /// </summary>
        public int MemberId { get; set; }
        public int StoreId { get; set; }
        public long ProductId { get; set; }
        public int IdentifyNum { get; set; }
        public List<int?>? ItemsId { get; set; }
        public int Qty { get; set; } = 1;
    }

    public static partial class CartAddVMExts
    {
        public static CartDTO ToCartDTO(this CartRequestVM source)
        {
            var cartDTO = new CartDTO
            {
                MemberId = source.MemberId,
                StoreId = source.StoreId,
                Details = new List<CartDetailDTO>()
            };

            var cartDetailDTO = new CartDetailDTO()
            {
                IdentifyNum = source.IdentifyNum,
                ProductId = source.ProductId,
                ItemsId = source.ItemsId,
                Qty = source.Qty
            };

            cartDTO.Details.Add(cartDetailDTO);

            return cartDTO;
        }
    }
}
