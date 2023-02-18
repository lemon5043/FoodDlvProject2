using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IDeliveryDriversRepository
    {
        bool DeliveryDriverExists(int id);
		Task<string> EditAsync(DeliveryDriverEditDTO model);
		Task<List<DeliveryDriverDTO>> GetDriversAsync();
		Task<(List<AccountStatueDTO>,
			List<DeliveryDriverWorkStatusDTO>)> GetListAsync();
		Task<DeliveryDriverDTO> GetOneAsync(int? id);
		Task<DeliveryDriverDTO> GetEditAsync(int? id);
	}
}