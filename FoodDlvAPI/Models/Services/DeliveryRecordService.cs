using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.EFModels;
using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.Services
{
    public class DeliveryRecordService
    {
        private readonly IDeliveryRecordsRepository _repository;

        public DeliveryRecordService(IDeliveryRecordsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DeliveryRecordDTO>> GetMonthlyRecordAsync(int? id)
            => await _repository.GetMonthlyRecordAsync(id);

        public async Task<List<DeliveryRecordDTO>> GetIndividualMonthlyRecordAsync(int? year, int? month, int? id)
            => await _repository.GetIndividualMonthlyRecordAsync(year, month, id);
    }
}
