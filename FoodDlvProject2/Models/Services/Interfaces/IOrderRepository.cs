using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.ViewModels;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// 篩選訂單
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderDto>> SearchAsync(DateTime? start, DateTime? end, string keyWord);

        /// <summary>
        /// 查詢訂單明細
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        IEnumerable<OrderDetailDto> DetailSearch(long orderId);

        /// <summary>
        /// 查詢商品資料
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IEnumerable<OrderProductDto> ProductSearch(long productId);
    }
}
