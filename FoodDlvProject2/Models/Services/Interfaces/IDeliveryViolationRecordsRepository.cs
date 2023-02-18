using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IDeliveryViolationRecordsRepository
    {
        Task<string> CreateAsync(DeliveryViolationRecordDTO model);
        Task<string> DeleteAsync(int? id);
        Task<string> EditAsync(DeliveryViolationRecordDTO model);
        Task<DeliveryViolationRecordDTO> GetDetailsAsync(int? id);
        Task<DeliveryViolationRecordDTO> GetEditAsync(int? id);
        Task<List<DeliveryViolationTypesDTO>> GetListAsync();
        Task<List<DeliveryViolationRecordDTO>> GetPersonalViolationRecordsAsync(int? id);
        Task<List<DeliveryViolationRecordDTO>> GetViolationRecords();
    }
}