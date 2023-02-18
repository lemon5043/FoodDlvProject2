using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;

namespace FoodDlvProject2.Models.Services
{
    public class DeliveryRecordService
	{
		private readonly IDeliveryRecordsRepository _repository;

		public DeliveryRecordService(IDeliveryRecordsRepository repository)
		{
			_repository = repository;
		}

        public async Task<List<DeliveryRecordDTO>> GetAllRecordAsync()
            => await _repository.GetAllRecordAsync();

        public async Task<List<DeliveryRecordDTO>> GetMonthlyRecordAsync(int? id)
            => await _repository.GetMonthlyRecordAsync(id);

        public async Task<List<DeliveryRecordDTO>> GetIndividualMonthlyRecordAsync(int? year, int? month, int? id)
            => await _repository.GetIndividualMonthlyRecordAsync(year, month, id);
    }
}
