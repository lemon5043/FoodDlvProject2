using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvProject2.Models.Repositories
{
    public class BenefitStandardsRepository : IBenefitStandardsRepository
	{
		private readonly AppDbContext db;

		public BenefitStandardsRepository(AppDbContext db)
		{
			this.db = db;
		}
		public async Task<IEnumerable<BenefitStandardsDTO>> GetBenefitStandardsAsync()
		{
			var query = db.BenefitStandards.Select(x => new BenefitStandardsDTO
			{
				Id = x.Id,
				PerOrder = x.PerOrder,
				PerMilage = x.PerMilage,
				Bouns1 = x.Bouns1,
				Bouns2 = x.Bouns2,
				Bouns3 = x.Bouns3,
				Selected = x.Selected,
			});
			return await query.Select(x => x.ToEntity()).ToListAsync();
		}

		public async Task<BenefitStandardsDTO> GetOneAsync(int? id)
		{
			if (db.BenefitStandards == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

			var query = db.BenefitStandards.Select(x => new BenefitStandardsDTO
			{
				Id = x.Id,
				PerOrder = x.PerOrder,
				PerMilage = x.PerMilage,
				Bouns1 = x.Bouns1,
				Bouns2 = x.Bouns2,
				Bouns3 = x.Bouns3,
				//HolidayBouns = source.HolidayBouns,
				//RushHoursBouns = source.RushHoursBouns,
				//RushHoursStart1 = source.RushHoursStart1,
				//RushHoursStart2 = source.RushHoursStart2,
				//RushHoursEnd1 = source.RushHoursEnd1,
				//RushHoursEnd2 = source.RushHoursEnd2,
				Selected = x.Selected,
			}).FirstOrDefaultAsync(m => m.Id == id);

			if (query == null) throw new Exception("很抱歉找不到相關的資料");

			return await query;
		}

		public async void CreateAsync(BenefitStandardsDTO model)
		{
			try
			{
				if (model.Selected)
				{
					db.Update(BenefitStandardSelected());
					await db.SaveChangesAsync();
				}
				db.Add(ToEFModle(model));
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BenefitStandardExists(model.Id)) throw new Exception("在更新資料時發生衝突。這可能是因為其他使用者已經更新了相同的資料，請重新載入頁面後再進行修改。");
			}
		}

		public async Task<string> EditAsync(BenefitStandardsDTO model)
		{
			try
			{
				var EFModel = ToEFModle(model);
				db.Attach(EFModel);
				string[] updateModel = { "PerOrder", "PerMilage", "Bouns1", "Bouns2", "Bouns3", "Selected", };

				foreach (var property in updateModel)
				{
					db.Entry(EFModel).Property(property).IsModified = true;
				}

				if (model.Selected)
				{
					List<BenefitStandard> list = new List<BenefitStandard> { EFModel, BenefitStandardSelected() };
					foreach (var item in list)
					{
						var existing = db.BenefitStandards.Find(item.Id);
						if (existing != null)
						{
							db.Entry(existing).CurrentValues.SetValues(item);
						}
					}
				}
				else
				{
					db.Update(EFModel);
				}
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BenefitStandardExists(model.Id)) throw new Exception("在更新資料時發生衝突。這可能是因為其他使用者已經更新了相同的資料，請重新載入頁面後再進行修改。");
			}
			return "修改成功";
		}

		public bool BenefitStandardExists(int id)
		{
			return db.BenefitStandards.Any(e => e.Id == id);
		}

		private BenefitStandard ToEFModle(BenefitStandardsDTO model)
		{			
			return new BenefitStandard
			{
				Id = model.Id,
				PerOrder = model.PerOrder,
				PerMilage = model.PerMilage,
				BonusThreshold1 = model.BonusThreshold1,
				BonusThreshold2 = model.BonusThreshold2,
				BonusThreshold3 = model.BonusThreshold3,
				Bouns1 = model.Bouns1,
				Bouns2 = model.Bouns2,
				Bouns3 = model.Bouns3,
				//HolidayBouns = source.HolidayBouns,
				//RushHoursBouns = source.RushHoursBouns,
				//RushHoursStart1 = source.RushHoursStart1,
				//RushHoursStart2 = source.RushHoursStart2,
				//RushHoursEnd1 = source.RushHoursEnd1,
				//RushHoursEnd2 = source.RushHoursEnd2,
				Selected = model.Selected,
			};
		}

		private BenefitStandard? BenefitStandardSelected()
		{
			int? id = db.BenefitStandards.FirstOrDefault(e => e.Selected == true).Id;
			if (id == null) throw new Exception("找不到目前正在使用的方案，請確認資料庫"); ;
			var selectItem = db.BenefitStandards.Find(id);
			selectItem.Selected = false;
			return selectItem;
		}
	}
}
