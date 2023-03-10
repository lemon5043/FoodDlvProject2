using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// 把Cart資訊快照至OrderDTO
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="address"></param>
        /// <param name="fee"></param>
        /// <returns></returns>
        OrderDTO GetOrderInfo(long cartId, string address, int fee);

        /// <summary>
        /// 訂單建立時, 確認是否在商家營業時間
        /// </summary>
        /// <param name="storeId"></param>
        void CheckOutTime(int storeId);

        /// <summary>
        /// 從會員帳戶錢包扣除訂單金額
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="storeId"></param>
        /// <param name="fee"></param>
        void CashTransfer(int memberId, int storeId ,int fee);

        /// <summary>
        /// 創建新訂單, 訂單明細 與 訂單狀態
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="storeId"></param>
        /// <param name="fee"></param>
        /// <param name="address"></param>
        void CreateNewOrder(int memberId, int storeId, int fee, string address);

        /// <summary>
        /// 顯示訂單資訊與追蹤訂單目前狀態
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderDTO GetOrderTrack(long orderId);
    }
}
