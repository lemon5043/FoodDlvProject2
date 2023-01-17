using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using NuGet.Common;
using NuGet.Protocol.Core.Types;

namespace FoodDlvProject2.Models.Services
{
    public class StaffService
    {
        private readonly IStaffRepository repo;

        public StaffService(IStaffRepository repo)
        {
            this.repo = repo;
        }

        //public IEnumerable<StaffDto> Search(int? categoryId, string productName)
        //    => repo.Search(categoryId, productName, true);

        /// <summary>
        /// 登入功能BLL層，判斷帳密是否符合
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public  (bool IsSuccess, string? ErrorMessage) Login(string account, string password)
        {
            StaffDto staff = repo.GetByAccount(account);

            if (staff == null)
            {
                return (false, "帳密有誤");
            }

            return (staff.EncryptedPassword == password)
                ? (true, null)
                : (false, "帳密有誤");
        }
    }
}
