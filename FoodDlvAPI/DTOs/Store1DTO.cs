namespace FoodDlvAPI.DTOs
{
	public class Store1DTO
	{
		public int Id { get; set; }
		public int StorePrincipalId { get; set; }
		public string StoreName { get; set; }
		public string Address { get; set; }
		public string ContactNumber { get; set; }
		public string Photo { get; set; }

		public int? Distance { get; set; }

		public IEnumerable<string> CategoryName { get; set; }
	}
}
