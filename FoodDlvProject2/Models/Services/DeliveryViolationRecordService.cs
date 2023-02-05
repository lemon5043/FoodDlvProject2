using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;

namespace FoodDlvProject2.Models.Services
{
    public class DeliveryViolationRecordService
    {
        private readonly IDeliveryViolationRecordsRepository _repository;

        public DeliveryViolationRecordService(IDeliveryViolationRecordsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DeliveryViolationRecordDTO>> GetViolationRecords()
            => await _repository.GetViolationRecords();

        public async Task<List<DeliveryViolationRecordDTO>> GetPersonalViolationRecordsAsync(int? id)
            => await _repository.GetPersonalViolationRecordsAsync(id);

        public async Task<DeliveryViolationRecordDTO> GetDetailsAsync(int? id)
            => await _repository.GetDetailsAsync(id);

        public async Task<DeliveryViolationRecordDTO> GetEditAsync(int? id)
            => await _repository.GetEditAsync(id);

        public async Task<List<DeliveryViolationTypesDTO>> GetListAsync()
            => await _repository.GetListAsync();

        public async Task<string> EditAsync(DeliveryViolationRecordDTO model)
            => await _repository.EditAsync(model);

        public async Task<string> CreateAsync(DeliveryViolationRecordDTO model)
            => await _repository.CreateAsync(model);

        public async Task<string> DeleteAsync(int? id)
            => await _repository.DeleteAsync(id);
    }
}
