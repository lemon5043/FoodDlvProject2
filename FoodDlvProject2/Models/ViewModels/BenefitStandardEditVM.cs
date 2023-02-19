using FoodDlvProject2.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class BenefitStandardEditVM
	{
        public int Id { get; set; }
        [Display(Name = "送餐費")]
        [Required(ErrorMessage = "請輸入{0}")]
        [Range(1, 100,ErrorMessage = "請輸入{1}~{2}之間的數字")]
        public int? PerOrder { get; set; }
        [Display(Name = "距離費")]
        [Required(ErrorMessage = "請輸入{0}")]
        [Range(1, 100, ErrorMessage = "請輸入{1}~{2}之間的數字")]

        public int? PerMilage { get; set; }
        [Display(Name = "達標門檻1")]
        [Required(ErrorMessage = "請輸入{0}")]
        [Range(1, 100, ErrorMessage = "請輸入{1}~{2}之間的數字")]
        public int? BonusThreshold1 { get; set; }
        [Display(Name = "達標門檻2")]
        [Required(ErrorMessage = "請輸入{0}")]
        [Range(1, 200, ErrorMessage = "請輸入{1}~{2}之間的數字")]
        public int? BonusThreshold2 { get; set; }
        [Display(Name = "達標門檻3")]
        [Required(ErrorMessage = "請輸入{0}")]
        [Range(1, 10000, ErrorMessage = "請輸入{1}~{2}之間的數字")]
        public int? BonusThreshold3 { get; set; }
        [Display(Name = "達標獎勵1")]	
        [Required(ErrorMessage = "請輸入{0}")]
        [Range(1, 10000, ErrorMessage = "請輸入{1}~{2}之間的數字")]
        public int? Bouns1 { get; set; }
        [Display(Name = "達標獎勵2")]
        [Required(ErrorMessage = "請輸入{0}")]
        [Range(1, 10000, ErrorMessage = "請輸入{1}~{2}之間的數字")]
        public int? Bouns2 { get; set; }
        [Display(Name = "達標獎勵3")]
        [Required(ErrorMessage = "請輸入{0}")]
        [Range(1, 10000, ErrorMessage = "請輸入{1}~{2}之間的數字")]
        public int? Bouns3 { get; set; }
        //public double HolidayBouns { get; set; }
        //public double RushHoursBouns { get; set; }
        //public TimeSpan RushHoursStart1 { get; set; }
        //public TimeSpan RushHoursStart2 { get; set; }
        //public TimeSpan RushHoursEnd1 { get; set; }
        //public TimeSpan RushHoursEnd2 { get; set; }
        [Display(Name = "方案啟用")]
        public bool Selected { get; set; }
    }
	public static class BenefitStandardEditVMExts
	{
		public static BenefitStandardDTO ToBenefitStandardsDTO(this BenefitStandardEditVM source)
		{
			return new BenefitStandardDTO
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

		public static BenefitStandardEditVM ToBenefitStandardsEditVM(this BenefitStandardDTO source)
		{
			return new BenefitStandardEditVM
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
