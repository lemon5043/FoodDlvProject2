using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Repositories;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IMemberRepository
    {
        bool MemberExists(int id);
        void Edit(MemberEditDTO model);
        IEnumerable<MemberDTO> GetMembers();
        MemberDTO GetOnly(int? id);
       void Delete(Member model);
    }
}