using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.Services.Interfaces
{
    public interface IDeliveryRepository
    {
        void ChangeWorkingStatus(int dirverId);
        //void ChangeToOnline(int dirverId);
        Task<AasignmentOrderDTO> GetOrderDetail(int orderId);
        AasignmentOrderDTO NavigationToCustomer(int orderId);
        void NavigationToStore(int orderId);
        void OrderArrive(int orderId);
    }
}