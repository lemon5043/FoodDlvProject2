using FoodDlvAPI.Infrastructures;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.Infrastructures;
using FoodDlvAPI.Repositories;
using FoodDlvAPI.ViewModels;
using NuGet.Protocol.Core.Types;
namespace FoodDlvAPI.Services
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
		public async Task<LoginResponse> Login(string account, string password)
		{
			MemberRegisterDto member = _repository.Load(account);

			if (member == null)
			{
				return LoginResponse.Fail("帳密有誤");
			}

			string encryptedPW = HashUtility.ToSHA256(password, MemberRegisterDto.SALT);

			return (String.CompareOrdinal(member.Password, encryptedPW) == 0)
				? LoginResponse.Success(member.Id, member.LastName+member.FirstName, member.Password)
				: LoginResponse.Fail("帳密有誤");
		}
	}
}
