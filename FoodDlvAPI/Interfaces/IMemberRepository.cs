using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodDlvAPI.Interfaces
{
    public interface IMemberRepository
    {
        Task<string> CreateAsync(MemberRegisterDto model);
        Task<MemberDTO> GetmemberAsync(int? id);
        Task<MemberDTO> GetEditAsync(int? id);
        Task<string> EditAsync(MemberRegisterDto model);
		MemberRegisterDto Load(string account);
        bool MemberExists(int id);
		Task<string> GetKey(string APIName);
		//Task<GetMemberPositionDto> GetMemberPosition(int orderId);
        //Task<ActionResult<MemberLocationVM>> GetMemberPosition(int MemberId);
        //Task<List<double>> GetMemberLongitudeNLatitude(int MemberId);
		
	}
}
