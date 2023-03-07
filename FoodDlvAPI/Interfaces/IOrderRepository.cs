using FoodDlvAPI.DTOs;

namespace FoodDlvAPI.Interfaces
{
    public interface IOrderRepository
    {
        OrderDTO GetOrderInfo(long cartId, string address, int fee);

        void CheckOutTime(int storeId);

        void CashTransfer(long memberId, int storeId ,int fee);
    }
}
