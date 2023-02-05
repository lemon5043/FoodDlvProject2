using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvProject2.Models.Repositories
{
    public class DeliveryCancellationRecordRepository : IDeliveryCancellationRecordRepository
	{
		private readonly AppDbContext db;

		public DeliveryCancellationRecordRepository(AppDbContext db)
		{
			this.db = db;
		}

		public async Task<List<DriverCancellationRecordDTO>> GetCancellationRecordsAsync()
		{
			var query = await db.DriverCancellationRecords
			   .Select(x => new DriverCancellationRecordDTO
			   {
				   Id = x.Id,
				   OrderId = x.OrderId,
				   DriverId = x.DeliveryDriversId,
				   DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
				   Reason = x.Cancellation.Reason,
				   CancellationDate = x.CancellationDate,
			   }).ToListAsync();
			return query;
		}

		public async Task<List<DriverCancellationRecordDTO>> GetPersonalCancellationRecordsAsync(int? id)
		{
			if (db.DriverCancellationRecords == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

			var query = await db.DriverCancellationRecords
				.Where(m => m.DeliveryDriversId == id)
				.Select(x => new DriverCancellationRecordDTO
				{
					Id = x.Id,
					OrderId = x.OrderId,
					DriverId = x.DeliveryDriversId,
					DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
					Reason = x.Cancellation.Reason,
					Context = x.Cancellation.Content,
					CancellationDate = x.CancellationDate,
				}).ToListAsync();

			if (query == null) throw new Exception("很抱歉找不到相關的資料");

			return query;
		}

		public async Task<DriverCancellationRecordDTO> GetEditAsync(int? id)
		{
			if (db.DeliveryViolationRecords == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

			var query = await db.DriverCancellationRecords
				.Where(i => i.Id == id)
				.Select(x => new DriverCancellationRecordDTO
				{
					Id = x.Id,
					OrderId = x.OrderId,
					DriverId = x.DeliveryDriversId,
					DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
					CancellationId = x.CancellationId,
					CancellationDate = x.CancellationDate,
				})
				.FirstOrDefaultAsync();

			if (query == null) throw new Exception("很抱歉找不到相關的資料");

			return query;
		}

		public async Task<SelectList> GetListAsync(int? CancellationId = 1)
		{
			var selectList = await db.DriverCancellations.Select(x => x.ToEntity()).ToListAsync();
			return new SelectList(selectList.Select(x=>x.ToVM()), "Id", "Reason", CancellationId);
		}

		public async Task<string> EditAsync(DriverCancellationRecordDTO model)
		{
			try
			{
				var EFModel = model.ToEFModels();
				db.Attach(EFModel);
				string[] updateModel = { "CancellationId", "CancellationDate" };

				foreach (var property in updateModel)
				{
					db.Entry(EFModel).Property(property).IsModified = true;
				}

				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
			}
			return "修改成功";
		}
	}
}
