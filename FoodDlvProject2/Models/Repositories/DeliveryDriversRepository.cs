using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Versioning;

namespace FoodDlvProject2.Models.Repositories
{
    public class DeliveryDriversRepository : IDeliveryDriversRepository
    {
        private readonly AppDbContext db;

        public DeliveryDriversRepository(AppDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DeliveryDriverDTO> GetDrivers()
        {
            var query = db.DeliveryDrivers.Select(x => new DeliveryDriverDTO
            {
                Id = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Gender = x.Gender,
                AccountStatus = x.AccountStatus.Status,
                WorkStatuse = x.WorkStatuse.Status,
            });
            return query.Select(x => x.ToEntity());
        }

        public DeliveryDriverDTO GetOne(int? id)
        {
            if (db.DeliveryDrivers == null)throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = db.DeliveryDrivers.Select(x => new DeliveryDriverDTO
            {
                Id = x.Id,
                Account = x.Account,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Gender = x.Gender,
                Birthday = x.Birthday,
                Phone = x.Phone,
                Email = x.Email,
                BankAccount = x.BankAccount,
                AccountStatusId=x.AccountStatusId,
                AccountStatus = x.AccountStatus.Status,
                WorkStatuse = x.WorkStatuse.Status,
                WorkStatuseId=x.WorkStatuseId,
                DeliveryViolationRecords = x.DeliveryViolationRecords.Sum(x => x.DeliveryDriversId),
                DriverRating = x.Orders.Average(x => x.DriverRating),
                RegistrationTime = x.RegistrationTime,
                Idcard = x.Idcard,
                VehicleRegistration = x.VehicleRegistration,
                DriverLicense = x.DriverLicense,
            }).FirstOrDefault(m => m.Id == id);

            if (query == null) throw new Exception("很抱歉找不到相關的資料");
            
            return query;
        }

        public void Edit(DeliveryDriverEditDTO model)
        {
            try
            {
                //db.Update(model);
                var EFModel = ToEFModle(model);
                db.Attach(EFModel);
                string[] updateModel = { "LastName", "FirstName", "Gender", "Birthday", "Phone", "Email",
                    "BankAccount", "AccountStatusId","WorkStatuseId" };

                foreach (var property in updateModel)
                {
                    db.Entry(EFModel).Property(property).IsModified = true;
                }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryDriverExists(model.Id)) throw new Exception("在更新資料時發生衝突。這可能是因為其他使用者已經更新了相同的資料，請重新載入頁面後再進行修改。");
            }
        }

        public bool DeliveryDriverExists(int id)
        {
            return db.DeliveryDrivers.Any(e => e.Id == id);
        }

        public (IEnumerable<AccountStatueDTO>,
            IEnumerable<DeliveryDriverWorkStatusDTO>) GetList()
        {
            var query = db.AccountStatues.Select(x=>x.ToEntity());
            var query2 = db.DeliveryDriverWorkStatuses.Select(x => x.ToEntity());
            return (query, query2);
        }

        public DeliveryDriver ToEFModle(DeliveryDriverEditDTO model)
        {
            return new DeliveryDriver
            {
                Id = model.Id,
                AccountStatusId = model.AccountStatusId,
                WorkStatuseId = model.WorkStatuseId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Gender = model.Gender,
                BankAccount = model.BankAccount,
                Birthday = model.Birthday,
                Email = model.Email,
                Idcard = model.Idcard,
                VehicleRegistration = model.VehicleRegistration,
                DriverLicense = model.DriverLicense,
            };
        }
    }
}
