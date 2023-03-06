namespace FoodDlvAPI.Models.DTOs
{
	public class PaysDTO
	{
		public int Id { get; set; }

		public int DeliveryDriversId { get; set; }

		public string DriversName { get; set; }

		public int DeliveryCount { get; set; }

		public int TotalMilage { get; set; }

		public int Bouns { get; set; }

		public int TotalPay { get; set; }

		public DateOnly SettlementMonth { get; set; }
	}
}
