using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IBenefitStandardsRepository
    {
		Task<string> CreateAsync(BenefitStandardDTO model);
		Task<string> EditAsync(BenefitStandardDTO model);
		Task<IEnumerable<BenefitStandardDTO>> GetBenefitStandardsAsync();
		Task<BenefitStandardDTO> GetOneAsync (int? id);
		Task<string> DeleteAsync(int? id);
        int FindSelectBenefitStandard();
		void CancelSelection();
	}
}