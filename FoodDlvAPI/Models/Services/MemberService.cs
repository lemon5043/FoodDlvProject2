using FoodDlvAPI.Infrastructures;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.Infrastructures;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models.ViewModels;
using NuGet.Protocol.Core.Types;
using System.Net;

namespace FoodDlvAPI.Models.Services
{
    public class MemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> RegisterAsync(MemberRegisterDto model)
          => await _repository.CreateAsync(model);

        public async Task<MemberDTO> GetmemberAsync(int? id)
            => await _repository.GetmemberAsync(id);

        public async Task<MemberDTO> GetEditAsync(int? id)
            => await _repository.GetEditAsync(id);


        public async Task<string> EditAsync(MemberRegisterDto model)
        {

            return await _repository.EditAsync(model);
        }
	
		public async Task<MemberLoginresponse> Login(string account, string password)
        {
            MemberRegisterDto member = _repository.Load(account);

            if (member == null)
            {
                return MemberLoginresponse.Fail("帳密有誤");
            }

            string encryptedPW = HashUtility.ToSHA256(password, MemberEncryptedPassword.SALT);

            return string.CompareOrdinal(member.Password, encryptedPW) == 0
                ? MemberLoginresponse.Success(member.Id, member.LastName + member.FirstName, member.Password)
                : MemberLoginresponse.Fail("帳密有誤");
        }


        //public async Task MemberLocation(int MemberId)
        //    => await _repository.GetMemberPosition(MemberId);
        //public async Task<GetMemberPositionDto> GetMemberPosition(int orderId)
        //    => await _repository.GetMemberPosition(orderId);
        //public async Task GetMemberLongitudeNLatitude(int MemberId)
        //=>await _repository.GetMemberLongitudeNLatitude(MemberId);

		

	}
}

