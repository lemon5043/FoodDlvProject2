using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.ViewModels;
using X.PagedList;

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
		Task<OrderMainDto> GetOrderMain(string revenueRange, string exceptionOrderRange, string completedOrderRange);

		/// <summary>
		/// 獲取OrderTracking訂單查詢資訊
		/// </summary>
		/// <param name="dateStart">搜尋條件:起始時間</param>
		/// <param name="dateEnd">搜尋條件:結束時間</param>
		/// <param name="searchItem">搜尋條件:欄位查詢</param>
		/// <param name="keyWord">搜尋條件:該欄位的關鍵字</param>
		/// <param name="pageSize">分頁:每頁該有幾筆資料</param>
		/// <param name="pageNumber">頁數</param>
		/// <returns></returns>
		Task<IPagedList<OrderTrackingDto>> GetOrderTrackingAsync(DateTime? dateStart, DateTime? dateEnd,
																string searchItem, string keyWord,
																int pageSize, int pageNumber);

		/// <summary>
		/// 獲取訂單編號相對應的訂單狀態時間線
		/// </summary>
		/// <param name="id">搜尋條件:id</param>
		/// <returns></returns>
		Task<IEnumerable<OrderScheduleDto>> GetOrderScheduleAsync(long id);

		/// <summary>
		/// 獲取OrderDetail訂單明細資訊
		/// </summary>
		/// <param name="id">搜尋條件:訂單id</param>
		/// <returns></returns>
		Task<IEnumerable<OrderDetailDto>> GetOrderDetailAsync(long id);

		/// <summary>
		/// 獲取OrderProductDetail訂單產品資訊
		/// </summary>
		/// <param name="productId">搜尋條件:產品id</param>
		/// <returns></returns>
		Task<IEnumerable<OrderProductDetailDto>> GetOrderProductDetailAsync(long productId);
	}
}
