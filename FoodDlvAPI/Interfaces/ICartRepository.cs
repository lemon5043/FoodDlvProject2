using FoodDlvAPI.DTOs;
using FoodDlvAPI.ViewModels;

namespace FoodDlvAPI.Interfaces
{
    public interface ICartRepository
    {
        /// <summary>
        /// 回傳此顧客的購物車是否存在
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        bool IsExists(int memberId);

        /// <summary>
        /// 建立一筆購物車主檔紀錄
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        CartDTO CreateNewCart(int memberId);

        /// <summary>
        /// 若購物車不存在, 傳回錯誤提示
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        CartDTO Load(int memberId);
        
        /// <summary>
        /// 清空購物車, 並刪除Database中的購物車主檔與明細檔
        /// </summary>
        /// <param name="memberId"></param>
        void EmptyCart(int memberId);

        void Save(CartDTO cart);
    }
}
