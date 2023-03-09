using FoodDlvAPI.EFModels;
using FoodDlvAPI.Models.Entitys;

namespace FoodDlvAPI.Models.Services.Interfaces
{
    public interface IDeliveryDriversRepository
    {
        bool DeliveryDriverExists(int id);
        Task EditAsync(DeliveryDriverEntity model);
        Task<DeliveryDriverDTO> GetOneAsync(int? id);
        Task<DeliveryDriverDTO> GetEditAsync(int? id);
        Task CreateAsync(DeliveryDriverEntity model);
        DeliveryDriverEntity Load(string account);
        DeliveryDriverEntity GetByAccount(string account);
        bool AccountExists(string account);
    }
}