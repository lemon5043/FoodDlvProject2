namespace FoodDlvProject2.Models.ViewModels
{
	public class BenefitStandardsIndexVM
	{
		public int Id { get; set; }
		public int PerOrder { get; set; }
		public int PerMilage { get; set; }
		public int Bouns1 { get; set; }
		public int Bouns2 { get; set; }
		public int Bouns3 { get; set; }
		//public double HolidayBouns { get; set; }
		//public double RushHoursBouns { get; set; }
		public bool Selected { get; set; }
	}
}
