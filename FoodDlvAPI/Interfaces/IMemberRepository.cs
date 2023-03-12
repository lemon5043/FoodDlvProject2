using FoodDlvAPI.Models.DTOs;

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
		Task<GetMemberPositionDto> GetMemberPosition(int orderId);
        Task GetMemberPosition(MemberLocationDto location);
        Task<List<double>> GetMemberLongitudeNLatitude(int memberId);
		
	}
}
