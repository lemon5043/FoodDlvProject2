using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IStaffRepository
    {
        /// <summary>
        /// 判斷此用戶是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool IsExist(string account);

        StaffDto Load(int memberId);

        StaffDto GetByAccount(string account);

        void Update(StaffDto entity);

        void UpdatePassword(int staffId, string newPassword);
    }
}
