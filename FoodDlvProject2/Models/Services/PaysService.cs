using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;

namespace FoodDlvProject2.Models.Services
{
	public class PaysService
	{
		private readonly IPaysRepository _repository;

		public PaysService(IPaysRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<PaysDTO>> GetPaysAsync()
			=> await _repository.GetPaysAsync();

		public async Task<List<PaysDTO>> GetMonthlyDetailsAsync(int? id)
			=> await _repository.GetMonthlyDetailsAsync(id);

		public async Task<PaysDTO> GetIndividualMonthlyDetailsAsync(int? year, int? month, int? id)
			=> await _repository.GetIndividualMonthlyDetailsAsync(year, month, id);	
	}
}
