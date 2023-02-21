using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.EFModels;
using System.Linq.Expressions;
using FoodDlvAPI.Models.Entitys;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Infrastructures;
using NuGet.Protocol.Core.Types;

namespace FoodDlvAPI.Models.Services
{
    public class DeliveryDriverService
    {
        private readonly IDeliveryDriversRepository _repository;

        public DeliveryDriverService(IDeliveryDriversRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeliveryDriverDTO> GetOneAsync(int? id)
            => await _repository.GetOneAsync(id);


        public async Task<DeliveryDriverDTO> GetEditAsync(int? id)
            => await _repository.GetEditAsync(id);

        public async Task<string> EditAsync(DeliveryDriverEntity model)
        {
            //if (model.Idcard == null || model.VehicleRegistration == null || model.DriverLicense == null)
            //{
            //    if (model.AccountStatusId == 2) throw new Exception("文件未備齊的帳號不能授權啟用，請再次檢查帳號狀態");
            //}
            return await _repository.EditAsync(model);
        }

        public async Task<string> CreateAsync(DeliveryDriverEntity model)
            => await _repository.CreateAsync(model);

        public async Task<LoginResponse> Login(string account, string password)
        {
            DeliveryDriverEntity member = _repository.Load(account);

            if (member == null)
            {
                return LoginResponse.Fail("帳密有誤");
            }

            if (!member.IsConfirmed)
            {
                return LoginResponse.Fail("會員資格尚未確認");
            }

            string encryptedPwd = HashUtility.ToSHA256(password, MemberEntity.SALT);

            return (String.CompareOrdinal(member.Password, encryptedPwd) == 0)
                ? LoginResponse.Success()
                : LoginResponse.Fail("帳密有誤");
        }
    }
}
