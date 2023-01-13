using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IDeliveryDriversRepository
    {
        bool DeliveryDriverExists(int id);
        void Edit(DeliveryDriverDTO model);
        IEnumerable<DeliveryDriverDTO> GetDrivers();
        DeliveryDriverDTO GetOne(int? id);
    }
}