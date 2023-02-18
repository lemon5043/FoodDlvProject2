using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDlvProject2.Models.Services
{
	public class DeliveryCancellationRecordService
	{
		private readonly IDeliveryCancellationRecordRepository _repository;

		public DeliveryCancellationRecordService(IDeliveryCancellationRecordRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<DriverCancellationRecordDTO>> GetCancellationRecordsAsync()
			=> await _repository.GetCancellationRecordsAsync();

		public async Task<List<DriverCancellationRecordDTO>> GetPersonalCancellationRecordsAsync(int? id)
			=> await _repository.GetPersonalCancellationRecordsAsync(id);

		public async Task<DriverCancellationRecordDTO> GetEditAsync(int? id)
			=> await _repository.GetEditAsync(id);

		public async Task<SelectList> GetListAsync(int? CancellationId = 1)
			=> await _repository.GetListAsync(CancellationId);

		public async Task<string> EditAsync(DriverCancellationRecordDTO model)
			=> await _repository.EditAsync(model);
	}
}
