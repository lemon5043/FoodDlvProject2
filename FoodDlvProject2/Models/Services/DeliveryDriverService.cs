using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using System.Linq.Expressions;

namespace FoodDlvProject2.Models.Services
{
    public class DeliveryDriverService
    {
        private readonly IDeliveryDriversRepository _repository;

        public DeliveryDriverService(IDeliveryDriversRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DeliveryDriverDTO>> GetDeliversAsync()
            => await _repository.GetDriversAsync();

        public async Task<DeliveryDriverDTO> GetOneAsync(int? id)
            => await _repository.GetOneAsync(id);
        

		public async Task<DeliveryDriverDTO> GetEditAsync(int? id)
			=> await _repository.GetEditAsync(id);

		public async Task<string> EditAsync(DeliveryDriverEditDTO model)
        {
            //if (model.Idcard == null || model.VehicleRegistration == null || model.DriverLicense == null)
            //{
            //    if (model.AccountStatusId == 2) throw new Exception("文件未備齊的帳號不能授權啟用，請再次檢查帳號狀態");
            //}
            return await _repository.EditAsync(model);
        }

        public async Task<(List<AccountStatueDTO>, List<DeliveryDriverWorkStatusDTO>)> GetListAsync()
            => await _repository.GetListAsync();
    }
}
