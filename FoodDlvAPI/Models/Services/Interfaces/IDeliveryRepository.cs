using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.Services.Interfaces
{
    public interface IDeliveryRepository
    {
        void ChangeWorkingStatus(int dirverId);
        //void ChangeToOnline(int dirverId);
        Task<AasignmentOrderDTO> GetOrderDetail(int orderId);
        Task<AasignmentOrderDTO> NavigationToCustomer(int orderId);
        Task<AasignmentOrderDTO> NavigationToStore(int orderId);
        Task MarkOrderStatus(int orderId);
        void ChangeDeliveryStatus(int dirverId);
    }
}