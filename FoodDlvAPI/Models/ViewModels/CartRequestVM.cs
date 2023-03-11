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
        public List<int?>? ItemId { get; set; }
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
                Details = new List<CartDetailDTO>().Select(cd => new CartDetailDTO
                {
                    IdentifyNum = source.IdentifyNum,
                    ProductId = source.ProductId,
                    ItemsId = source.ItemId,
                    Qty = source.Qty
                }).ToList(),
            };
            return cartDTO;
        }
    }
}
