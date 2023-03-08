using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Versioning;
using System.IO;

namespace FoodDlvProject2.Models.Repositories
{
    public class DeliveryDriversRepository : IDeliveryDriversRepository
    {
        private readonly AppDbContext db;

        public DeliveryDriversRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<DeliveryDriverDTO>> GetDriversAsync()
        {
            var query = db.DeliveryDrivers.Select(x => new DeliveryDriverDTO
            {
                Id = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                AccountStatus = x.AccountStatus.Status,
                WorkStatuse = x.WorkStatuse.Status,
            });
            return await query.Select(x => x.ToEntity()).ToListAsync();
        }

        public async Task<DeliveryDriverDTO> GetOneAsync(int? id)
        {
            if (db.DeliveryDrivers == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.DeliveryDrivers.Select(x => new DeliveryDriverDTO
            {
                Id = x.Id,
                Account = x.Account,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Phone = x.Phone,
                BankAccount = x.BankAccount,
                AccountStatusId = x.AccountStatusId,
                AccountStatus = x.AccountStatus.Status,
                WorkStatuse = x.WorkStatuse.Status,
                WorkStatuseId = x.WorkStatuseId,
                DeliveryViolationRecords = x.DeliveryViolationRecords.Count(),
                DriverRating = x.Orders.Average(x => x.DriverRating),
                RegistrationTime = x.RegistrationTime,
                Idcard = x.Idcard,
                VehicleRegistration = x.VehicleRegistration,
                DriverLicense = x.DriverLicense,
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

        public async Task<DeliveryDriverDTO> GetEditAsync(int? id)
        {
            if (db.DeliveryDrivers == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");
            //string path = "~/img/DeliveyDriver/";
            var query = await db.DeliveryDrivers.Select(x => new DeliveryDriverDTO
            {
                Id = x.Id,
                Account = x.Account,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Phone = x.Phone,
                BankAccount = x.BankAccount,
                AccountStatusId = x.AccountStatusId,
                WorkStatuseId = x.WorkStatuseId,
                Idcard = x.Idcard,
                VehicleRegistration = x.VehicleRegistration,
                DriverLicense = x.DriverLicense,
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

        public async Task<string> EditAsync(DeliveryDriverEditDTO model)
        {
            string? idCard = await UploadFile(model.Idcard, "Idcard", model.Id);
            string? VehicleRegistration = await UploadFile(model.VehicleRegistration, "VehicleRegistration", model.Id);
            string? DriverLicense = await UploadFile(model.DriverLicense, "DriverLicense", model.Id);
            try
            {
                //db.Update(model);
                var EFModel = model.ToEFModle();
                List<string> updateModel = new List<string> { "LastName", "FirstName", "Phone", "Account",
                    "BankAccount", "AccountStatusId","WorkStatuseId"};

                if (idCard != null)
                {
                    EFModel.Idcard = idCard;
                    updateModel.Add("Idcard");
                }
                if (VehicleRegistration != null)
                {
                    EFModel.VehicleRegistration = VehicleRegistration;
                    updateModel.Add("VehicleRegistration");
                }
                if (DriverLicense != null)
                {
                    EFModel.DriverLicense = DriverLicense;
                    updateModel.Add("DriverLicense");
                }
                db.Attach(EFModel);


                foreach (var property in updateModel)
                {
                    db.Entry(EFModel).Property(property).IsModified = true;
                }

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryDriverExists(model.Id)) throw new Exception("在更新資料時發生衝突。這可能是因為其他使用者已經更新了相同的資料，請重新載入頁面後再進行修改。");
            }

            return "修改成功";
        }

        public bool DeliveryDriverExists(int id)
        {
            return db.DeliveryDrivers.Any(e => e.Id == id);
        }

        public async Task<(List<AccountStatueDTO>,
            List<DeliveryDriverWorkStatusDTO>)> GetListAsync()
        {
            var query = await db.AccountStatues.Select(x => x.ToEntity()).ToListAsync();
            var query2 = await db.DeliveryDriverWorkStatuses.Select(x => x.ToEntity()).ToListAsync();
            return (query, query2);
        }

        public async Task<string?> UploadFile(IFormFile file, string folder, int id)
        {
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                string newFileName = id.ToString() + extension;
                string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../food-dlv-website/src/assets/images/public/DeliveyDriver", folder));
                string path = Path.Combine(filePath, newFileName);
                using var fileStream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(fileStream);
                return newFileName;
            }
            return null;
        }

    }
}
