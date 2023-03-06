using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.EFModels;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Versioning;
using System.IO;
using FoodDlvAPI.Models.Entitys;

namespace FoodDlvAPI.Models.Repositories
{
    public class DeliveryDriversRepository : IDeliveryDriversRepository
    {
        private readonly AppDbContext db;

        public DeliveryDriversRepository(AppDbContext db)
        {
            this.db = db;
        }

        public DeliveryDriverEntity Load(string account)
            => db.DeliveryDrivers
                .SingleOrDefault(x => x.Account == account).ToEntity();

        public DeliveryDriverEntity GetByAccount(string account)
        {
            return db.DeliveryDrivers.SingleOrDefault(x => x.Account == account).ToEntity();
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

            var query = await db.DeliveryDrivers.Select(x => new DeliveryDriverDTO
            {
                Id = x.Id,
                Account = x.Account,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Phone = x.Phone,
                BankAccount = x.BankAccount,
                AccountStatusId = x.AccountStatusId,
                Idcard = x.Idcard,
                VehicleRegistration = x.VehicleRegistration,
                DriverLicense = x.DriverLicense,
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

        public async Task<string> CreateAsync(DeliveryDriverEntity model)
        {
            string? idCard = await UploadFile(model.Idcard, "Idcard", null);
            string? VehicleRegistration = await UploadFile(model.VehicleRegistration, "VehicleRegistration", null);
            string? DriverLicense = await UploadFile(model.DriverLicense, "DriverLicense", null);
            try
            {
                //db.Update(model);
                var EFModel = model.ToEFModle();
                //List<string> updateModel = new List<string> { "Account","Password","LastName", "FirstName", "Gender", "Birthday", "Phone", "Email",
                //"BankAccount","RegistrationTime"};

                if (idCard != null)
                {
                    EFModel.Idcard = idCard;
                    //updateModel.Add("Idcard");
                }
                if (VehicleRegistration != null)
                {
                    EFModel.VehicleRegistration = VehicleRegistration;
                    //updateModel.Add("VehicleRegistration");
                }
                if (DriverLicense != null)
                {
                    EFModel.DriverLicense = DriverLicense;
                    //updateModel.Add("DriverLicense");
                }
                db.DeliveryDrivers.Add(EFModel);

                //foreach (var property in updateModel)
                //{
                //    db.Entry(EFModel).Property(property).IsModified = true;
                //}

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (AccountExists(model.Account)) throw new Exception("此信箱已被使用，請使用其他信箱進行申請");
            }

            return "新增成功";
        }

        public async Task<string> EditAsync(DeliveryDriverEntity model)
        {
            string? idCard = await UploadFile(model.Idcard, "Idcard", model.Id);
            string? VehicleRegistration = await UploadFile(model.VehicleRegistration, "VehicleRegistration", model.Id);
            string? DriverLicense = await UploadFile(model.DriverLicense, "DriverLicense", model.Id);
            try
            {
                var EFModel = model.ToEFModle();
                List<string> updateModel = new List<string> { "LastName", "FirstName", "Gender", "Birthday", "Phone", "Email",
                    "BankAccount"};

                if (idCard != null)
                {
                    EFModel.Idcard = idCard;
                    updateModel.Add("IdCard");
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
                db.DeliveryDrivers.Attach(EFModel);


                foreach (var property in updateModel)
                {
                    db.Entry(EFModel).Property(property).IsModified = true;
                }

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryDriverExists(model.Id)) throw new Exception("在新增資料時發生衝突，請重新載入頁面後再進行新增。");
            }

            return "修改成功";
        }

        public bool DeliveryDriverExists(int id)
        {
            return db.DeliveryDrivers.Any(e => e.Id == id);
        }

        public bool AccountExists(string account)
        {
            return db.DeliveryDrivers.Any(e => e.Account == account);
        }

        public async Task<(List<AccountStatueDTO>,
            List<DeliveryDriverWorkStatusDTO>)> GetListAsync()
        {
            var query = await db.AccountStatues.Select(x => x.ToEntity()).ToListAsync();
            var query2 = await db.DeliveryDriverWorkStatuses.Select(x => x.ToEntity()).ToListAsync();
            return (query, query2);
        }

        public async Task<string?> UploadFile(IFormFile file, string folder, int? id)
        {
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                int ImgId = (int)((id != null) ? id : db.DeliveryDrivers.OrderBy(x => x.Id).Max(x => x.Id) + 1);
                string newFileName = ImgId.ToString() + extension;
                string filePath = Path.GetFullPath(Path.Combine("../FoodDlvProject2/wwwroot/img/DeliveyDriver", folder));
                string path = Path.Combine(filePath, newFileName);
                using var fileStream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(fileStream);
                return newFileName;
            }
            return null;
        }


    }
}
