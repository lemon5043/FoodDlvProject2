using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace FoodDlvProject2.Models.Repositories
{
    public class DeliveryViolationRecordsRepository : IDeliveryViolationRecordsRepository
    {
        private readonly AppDbContext db;

        public DeliveryViolationRecordsRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<DeliveryViolationRecordDTO>> GetViolationRecords()
        {
            var query = await db.DeliveryViolationRecords
                .Select(x => new DeliveryViolationRecordDTO
                {
                    Id = x.Id,
                    DeliveryDriversId = x.DeliveryDriversId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    OrderId = x.OrderId,
                    ViolationContent = x.Violation.ViolationContent,
                    ViolationDate = x.ViolationDate,
                }).ToListAsync();

            return query;
        }

        public async Task<List<DeliveryViolationRecordDTO>> GetPersonalViolationRecordsAsync(int? id)
        {
            if (db.DeliveryViolationRecords == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.DeliveryViolationRecords
                .Where(m => m.DeliveryDriversId == id)
                .Select(x => new DeliveryViolationRecordDTO
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    ViolationContent = x.Violation.ViolationContent,
                    Content = x.Violation.ViolationContent,
                    ViolationDate = x.ViolationDate,
                }).ToListAsync();

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

        public async Task<DeliveryViolationRecordDTO> GetDetailsAsync(int? id)
        {
            if (db.DeliveryViolationRecords == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.DeliveryViolationRecords
               .Where(m => m.Id == id)
               .Select(x => new DeliveryViolationRecordDTO
               {
                   Id = x.Id,
                   OrderId = x.OrderId,
                   DeliveryDriversId = x.DeliveryDriversId,
                   DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                   ViolationContent = x.Violation.ViolationContent,
                   Content = x.Violation.ViolationContent,
                   ViolationDate = x.ViolationDate,
               }).FirstOrDefaultAsync();

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

        public async Task<DeliveryViolationRecordDTO> GetEditAsync(int? id)
        {
            if (db.DeliveryViolationRecords == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.DeliveryViolationRecords
               .Where(m => m.Id == id)
               .Select(x => new DeliveryViolationRecordDTO
               {
                   Id = x.Id,
                   OrderId = x.OrderId,
                   DeliveryDriversId= x.DeliveryDriversId,
                   DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                   ViolationId = x.ViolationId,
                   ViolationDate = x.ViolationDate,
               }).FirstOrDefaultAsync();

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

        public async Task<List<DeliveryViolationTypesDTO>> GetListAsync()
            => await db.DeliveryViolationTypes.Select(x => x.ToEntity()).ToListAsync();

        public async Task<string> EditAsync(DeliveryViolationRecordDTO model)
        {
            try
            {
                var EFModel = model.ToEFModel();
                db.Attach(EFModel);
                string[] updateModel = { "ViolationId", "ViolationDate" };

                foreach (var property in updateModel)
                {
                    db.Entry(EFModel).Property(property).IsModified = true;
                }

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViolationRecordExists(model.OrderId)) throw new Exception("在更新資料時發生衝突。這可能是因為其他使用者已經更新了相同的資料，請重新載入頁面後再進行修改。");
            }

            return "修改成功";
        }

        private bool ViolationRecordExists(long id)
        {
            return db.DeliveryViolationRecords.Any(e => e.OrderId == id);
        }

        public async Task<string> CreateAsync(DeliveryViolationRecordDTO model)
        {
            try
            {
                db.Add(model.ToEFModel());
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViolationRecordExists(model.OrderId)) throw new Exception("在更新資料時發生衝突。這可能是因為其他使用者已經更新了相同的資料，請重新載入頁面後再進行修改。");
            }

            return "新增成功";
        }

        public async Task<string> DeleteAsync(int? id)
        {
            if (db.DeliveryViolationRecords == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var DeliveryViolationRecords = await db.DeliveryViolationRecords.FindAsync(id);

            if (DeliveryViolationRecords != null)
            {
                db.DeliveryViolationRecords.Remove(DeliveryViolationRecords);
            }
            else
            {
                return "指定刪除項目不存在，請再確認一次紀錄是否存在";
            }

            await db.SaveChangesAsync();

            return "刪除成功";
        }
    }
}
