using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IDeliveryDriversRepository
    {
        bool DeliveryDriverExists(int id);
        void Edit(DeliveryDriverEditDTO model);
        IEnumerable<DeliveryDriverDTO> GetDrivers();
        (IEnumerable<AccountStatueDTO>, IEnumerable<DeliveryDriverWorkStatusDTO>) GetList();
        DeliveryDriverDTO GetOne(int? id);
    }
}