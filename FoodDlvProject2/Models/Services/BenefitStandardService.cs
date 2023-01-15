using AspNetCore;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;

namespace FoodDlvProject2.Models.Services
{
	public class BenefitStandardService
	{
		private readonly IBenefitStandardsRepository _repository;

		public BenefitStandardService(IBenefitStandardsRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<BenefitStandardsDTO>> GetBenefitStandardsAsync()
			=> await _repository.GetBenefitStandardsAsync();

		public async Task<BenefitStandardsDTO> GetOneAsync(int? id)
			=> await _repository.GetOneAsync(id);

		public async void CreateAsync(BenefitStandardsDTO model)
			=> _repository.CreateAsync(model);

		public async Task<string> EditAsync(BenefitStandardsDTO model)
			=> await _repository.EditAsync(model);

	}
}
