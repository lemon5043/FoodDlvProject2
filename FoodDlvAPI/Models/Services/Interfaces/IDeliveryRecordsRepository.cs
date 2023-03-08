using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.Services.Interfaces
{
    public interface IDeliveryRecordsRepository
    {
        Task<List<DeliveryRecordDTO>> GetIndividualMonthlyRecordAsync(int? year, int? month, int? id);
        Task<List<DeliveryRecordDTO>> GetMonthlyRecordAsync(int? id);
    }
}