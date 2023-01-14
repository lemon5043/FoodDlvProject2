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

        public IEnumerable<DeliveryDriverDTO> GetDelivers()
            => _repository.GetDrivers();

        public DeliveryDriverDTO GetOne(int? id)
        {
            return _repository.GetOne(id);
        }

        public void Edit(DeliveryDriverEditDTO model)
        {
            if (model.Idcard == null || model.VehicleRegistration == null || model.DriverLicense == null)
            {
                if (model.AccountStatusId == 2) throw new Exception("文件未備齊的帳號不能授權啟用，請再次檢查帳號狀態");
            }
            _repository.Edit(model);
        }

        public (IEnumerable<AccountStatueDTO>, IEnumerable<DeliveryDriverWorkStatusDTO>) GetList()
            => _repository.GetList();
    }
}
