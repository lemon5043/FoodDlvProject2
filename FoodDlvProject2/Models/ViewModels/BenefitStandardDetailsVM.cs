using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
	public class BenefitStandardDetailsVM
	{
        public int Id { get; set; }
        [Display(Name = "送餐費")]
        public int PerOrder { get; set; }
        [Display(Name = "距離費")]
        public int PerMilage { get; set; }
        [Display(Name = "達標門檻1")]
        public int BonusThreshold1 { get; set; }
        [Display(Name = "達標門檻2")]
        public int BonusThreshold2 { get; set; }
        [Display(Name = "達標門檻3")]
        public int BonusThreshold3 { get; set; }
        [Display(Name = "達標獎勵1")]
        public int Bouns1 { get; set; }
        [Display(Name = "達標獎勵2")]
        public int Bouns2 { get; set; }
        [Display(Name = "達標獎勵3")]
        public int Bouns3 { get; set; }
        //public double HolidayBouns { get; set; }
        //public double RushHoursBouns { get; set; }
        //public TimeSpan RushHoursStart1 { get; set; }
        //public TimeSpan RushHoursStart2 { get; set; }
        //public TimeSpan RushHoursEnd1 { get; set; }
        //public TimeSpan RushHoursEnd2 { get; set; }
        [Display(Name = "方案啟用")]
        public bool Selected { get; set; }
    }

	public static class BenefitStandardsDetailsExts
	{
		public static BenefitStandardDetailsVM ToBenefitStandardsDetailsVM(this BenefitStandardDTO source)
		{
			return new BenefitStandardDetailsVM
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
				Selected = source.Selected
			};
		}
	}
}
