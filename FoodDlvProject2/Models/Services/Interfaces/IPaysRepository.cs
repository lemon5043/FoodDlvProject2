using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IPaysRepository
    {
        Task<List<PaysDTO>> GetPaysAsync();
        Task<PaysDTO> GetIndividualMonthlyDetailsAsync(int? year, int? month, int? id);
        Task<List<PaysDTO>> GetMonthlyDetailsAsync(int? id);
    }
}