using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvAPI.Models.Repositories
{
    public class DeliveryRecordsRepository : IDeliveryRecordsRepository
    {
        private readonly AppDbContext db;

        public DeliveryRecordsRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<DeliveryRecordDTO>> GetAllRecordAsync()
        {
            var data = await db.Orders
                .Join(db.OrderSchedules.Where(x => x.StatusId > 3), o => o.Id, s => s.OrderId, (o, s) => new
                {
                    o.Id,
                    o.Milage,
                    s.MarkTime,
                    s.Status.Status,
                    o.DeliveryDrivers.FirstName,
                    o.DeliveryDrivers.LastName,
                    o.DeliveryDriversId,
                }).ToListAsync();

            var query = data.GroupBy(r => r.Id)
               .Select(g => g.OrderByDescending(r => r.MarkTime).FirstOrDefault())
               .Select(s => new DeliveryRecordDTO
               {
                   Id = s.Id,
                   OrderDate = s.MarkTime,
                   Milage = s.Milage,
                   Status = s.Status.ToString(),
                   DriverName = s.LastName + s.FirstName,
                   DeliveryDriversId = s.DeliveryDriversId
               }).ToList();

            return query;
        }

        public async Task<List<DeliveryRecordDTO>> GetMonthlyRecordAsync(int? id)
        {
            if (id == null || db.Orders == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var data = await db.Orders
                .Where(m => m.DeliveryDriversId == id)
                .Join(db.OrderSchedules.Where(s => s.StatusId > 3), o => o.Id, s => s.OrderId, (o, s) => new
                {
                    o.Id,
                    o.Milage,
                    s.MarkTime,
                    o.DeliveryDrivers.FirstName,
                    o.DeliveryDrivers.LastName,
                }).ToListAsync();

            var query = data.GroupBy(r => r.Id)
               .Select(g => g.OrderByDescending(r => r.MarkTime)
               .FirstOrDefault())
               .Select(s => new
               {
                   s.Id,
                   OrderDate = s.MarkTime,
                   s.Milage,
                   DriverName = s.LastName + s.FirstName,
               })
               .GroupBy(r => r.OrderDate.Month)
               .Select(s => new DeliveryRecordDTO
               {
                   TotalMilage = s.Sum(x => x.Milage),
                   TotalDelievery = s.Count(),
                   OrderDate = s.Min(x => x.OrderDate),
                   DriverName = s.Select(x => x.DriverName).FirstOrDefault(),
               }).ToList();

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

        public async Task<List<DeliveryRecordDTO>> GetIndividualMonthlyRecordAsync(int? year, int? month, int? id)
        {
            if (year == null || month == null || id == null || db.Orders == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var data = await db.Orders
                .Include(d => d.DeliveryDrivers)
                .Where(m => m.DeliveryDriversId == id)
                .Join(db.OrderSchedules.Where(s => s.StatusId > 3), o => o.Id, s => s.OrderId, (o, s) => new
                {
                    o.Id,
                    o.Milage,
                    s.MarkTime,
                    s.Status.Status,
                    o.DeliveryDrivers.FirstName,
                    o.DeliveryDrivers.LastName,
                }).ToListAsync();

            var query = data.GroupBy(r => r.Id)
                .Select(g => g.OrderByDescending(r => r.MarkTime)
                .FirstOrDefault())
                .Where(x => x.MarkTime.Year == year && x.MarkTime.Month == month)
                .Select(s => new DeliveryRecordDTO
                {
                    Id = s.Id,
                    OrderDate = s.MarkTime,
                    Milage = s.Milage,
                    Status = s.Status.ToString(),
                    DriverName = s.LastName + s.FirstName,
                }).ToList();

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }
    }
}
