using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IDeliveryRecordsRepository
    {
        Task<List<DeliveryRecordDTO>> GetAllRecordAsync();
        Task<List<DeliveryRecordDTO>> GetIndividualMonthlyRecordAsync(int? year, int? month, int? id);
        Task<List<DeliveryRecordDTO>> GetMonthlyRecordAsync(int? id);
    }
}