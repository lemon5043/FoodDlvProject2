using FoodDlvProject2.EFModels;
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
            if (id == null || db.DeliveryDrivers == null)
            {
                return null;
            }

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
                DeliveryViolationRecords = x.DeliveryViolationRecords.Sum(x => x.DeliveryDriversId),
                DriverRating = x.Orders.Average(x => x.DriverRating),
                RegistrationTime = x.RegistrationTime,
                Idcard = x.Idcard,
                VehicleRegistration = x.VehicleRegistration,
                DriverLicense = x.DriverLicense,
            }).FirstOrDefault(m => m.Id == id);

            return query;
        }

        public void Edit(DeliveryDriverDTO model)
        {
            var deliveryDriverDB = db.DeliveryDrivers.Find(model.Id);
            db.Entry(deliveryDriverDB).CurrentValues.SetValues(model);
            db.SaveChanges();
        }

        public bool DeliveryDriverExists(int id)
        {
            return db.DeliveryDrivers.Any(e => e.Id == id);
        }
    }
}
