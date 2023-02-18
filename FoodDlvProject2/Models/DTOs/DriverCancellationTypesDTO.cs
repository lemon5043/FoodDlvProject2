using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.DTOs
{
	public class DriverCancellationTypesDTO
	{
		public int Id { get; set; }

		public string Reason { get; set; }

		public string Content { get; set; }
	}

	public static class DriverCancellationTypesExts
	{
		public static DriverCancellationTypesDTO ToEntity(this DriverCancellation source)
		{
			return new DriverCancellationTypesDTO
			{
				Id = source.Id,
				Reason = source.Reason,
				Content = source.Content,
			};
		}
	}
}
