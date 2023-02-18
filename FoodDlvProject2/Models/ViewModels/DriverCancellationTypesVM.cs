using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.ViewModels
{
	public class DriverCancellationTypesVM
	{
		public int Id { get; set; }

		public string Reason { get; set; }

		public string Content { get; set; }
	}

	public static class DriverCancellationTypesVMExts
	{
		public static DriverCancellationTypesVM ToVM(this DriverCancellationTypesDTO source)
		{
			return new DriverCancellationTypesVM
			{
				Id = source.Id,
				Reason = source.Reason,
				Content = source.Content,
			};
		}
	}
}
