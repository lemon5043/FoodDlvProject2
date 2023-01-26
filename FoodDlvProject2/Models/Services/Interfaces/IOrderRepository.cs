using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.ViewModels;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IOrderRepository
    {
		/// <summary>
		/// 獲取OrderMain資訊
		/// </summary>
		/// <param name="revenueRange">"時段範圍"用於選取該範圍營收金額</param>
		/// <param name="exceptionOrderRange">"時段範圍"用於選取該範圍例外訂單數量</param>
		/// <param name="completedOrderRange">"時段範圍"用於選取該範圍已完成訂單數量</param>
		/// <returns></returns>
		Task<IEnumerable<OrderMainDto>> GetOrderMain(string revenueRange, string exceptionOrderRange, string completedOrderRange);

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
