using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.EFModels;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FoodDlvAPI.Models.Repositories
{
    public class PaysRepository : IPaysRepository
    {
        private readonly AppDbContext db;

        public PaysRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<PaysDTO>> GetPaysAsync()
        {
            var query = await db.Pays.Select(x => new PaysDTO
            {
                Id = x.Id,
                DeliveryDriversId = x.DeliveryDriversId,
                DriversName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                DeliveryCount = x.DeliveryCount,
                TotalMilage = x.TotalMilage,
                Bouns = x.Bouns,
                TotalPay = x.TotalPay,
                SettlementMonth = x.SettlementMonth,
            }).ToListAsync();

            return query;
        }

        public async Task<List<PaysDTO>> GetMonthlyDetailsAsync(int? id)
        {
            if (db.DriverCancellationRecords == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.Pays
                .Select(x => new PaysDTO
                {
                    Id = x.Id,
                    DeliveryDriversId = x.DeliveryDriversId,
                    DriversName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    DeliveryCount = x.DeliveryCount,
                    TotalMilage = x.TotalMilage,
                    Bouns = x.Bouns,
                    TotalPay = x.TotalPay,
                    SettlementMonth = x.SettlementMonth,
                })
            .Where(m => m.DeliveryDriversId == id).ToListAsync();

            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

        public async Task<PaysDTO> GetIndividualMonthlyDetailsAsync(int? year, int? month, int? id)
        {
            if (db.DriverCancellationRecords == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.Pays
                .Select(x => new PaysDTO
                {
                    Id = x.Id,
                    DeliveryDriversId = x.DeliveryDriversId,
                    DriversName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    DeliveryCount = x.DeliveryCount,
                    TotalMilage = x.TotalMilage,
                    Bouns = x.Bouns,
                    TotalPay = x.TotalPay,
                    SettlementMonth = x.SettlementMonth,
                })
                .FirstOrDefaultAsync(m => m.DeliveryDriversId == id && m.SettlementMonth.Year == year && m.SettlementMonth.Month == month);


            if (query == null) throw new Exception("很抱歉找不到相關的資料");

            return query;
        }

    }
}
