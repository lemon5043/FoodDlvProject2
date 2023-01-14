using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IBenefitStandardsRepository
    {
        void CreateAsync(BenefitStandardsDTO model);
        void EditAsync(BenefitStandardsDTO model);
        IEnumerable<BenefitStandardsDTO> GetBenefitStandards();
        BenefitStandardsDTO GetOne(int? id);
    }
}