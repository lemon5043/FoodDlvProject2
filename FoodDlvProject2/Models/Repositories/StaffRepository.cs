using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Infrastructures.ExtensionMethods;
using FoodDlvProject2.Models.Services.Interfaces;

namespace FoodDlvProject2.Models.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private AppDbContext db = new AppDbContext();
        public StaffDto GetByAccount(string account)
        {
            return db.Staffs.SingleOrDefault(x => x.Account == account).ToDto();
        }

        public bool IsExist(string account)
        {
            var entity = db.Staffs.SingleOrDefault(x => x.Account == account);

            return (entity != null);
        }

        public StaffDto Load(int memberId)
        {
            throw new NotImplementedException();
        }

        public void Update(StaffDto entity)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassword(int staffId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
