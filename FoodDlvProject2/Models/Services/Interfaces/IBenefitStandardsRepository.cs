using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IBenefitStandardsRepository
    {
        void CreateAsync(BenefitStandardsDTO model);
		Task<string> EditAsync(BenefitStandardsDTO model);
		Task<IEnumerable<BenefitStandardsDTO>> GetBenefitStandardsAsync();
		Task<BenefitStandardsDTO> GetOneAsync (int? id);
    }
}