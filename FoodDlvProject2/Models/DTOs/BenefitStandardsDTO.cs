using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.DTOs
{
	public class BenefitStandardsDTO
	{
		public int Id { get; set; }
		public int PerOrder { get; set; }
		public int PerMilage { get; set; }
		public int BonusThreshold1 { get; set; }
		public int BonusThreshold2 { get; set; }
		public int BonusThreshold3 { get; set; }
		public int Bouns1 { get; set; }
		public int Bouns2 { get; set; }
		public int Bouns3 { get; set; }
		//public double HolidayBouns { get; set; }
		//public double RushHoursBouns { get; set; }
		//public TimeSpan RushHoursStart1 { get; set; }
		//public TimeSpan RushHoursStart2 { get; set; }
		//public TimeSpan RushHoursEnd1 { get; set; }
		//public TimeSpan RushHoursEnd2 { get; set; }
		public bool Selected { get; set; }
	}
	public static class BenefitStandardExts
	{
		public static BenefitStandardsDTO ToEntity(this BenefitStandardsDTO source)
		=>  new BenefitStandardsDTO
		{
			Id = source.Id,
			PerOrder = source.PerOrder,
			PerMilage = source.PerMilage,
			BonusThreshold1 = source.BonusThreshold1,
			BonusThreshold2 = source.BonusThreshold2,
			BonusThreshold3 = source.BonusThreshold3,
			Bouns1 = source.Bouns1,
			Bouns2 = source.Bouns2,
			Bouns3 = source.Bouns3,
			//HolidayBouns = source.HolidayBouns,
			//RushHoursBouns = source.RushHoursBouns,
			//RushHoursStart1 = source.RushHoursStart1,
			//RushHoursStart2 = source.RushHoursStart2,
			//RushHoursEnd1 = source.RushHoursEnd1,
			//RushHoursEnd2 = source.RushHoursEnd2,
			Selected = source.Selected,
		};
	}
}
