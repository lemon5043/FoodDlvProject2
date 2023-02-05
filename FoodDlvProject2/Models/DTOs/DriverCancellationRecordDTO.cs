using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.DTOs
{
	public class DriverCancellationRecordDTO
	{
		public int Id { get; set; }
		
		public long OrderId { get; set; }
		
		public int DriverId { get; set; }
		
		public string DriverName { get; set; }

		public int CancellationId { get; set; }

		public string Reason { get; set; }
		
		public string Context { get; set; }
		
		public DateTime CancellationDate { get; set; }
	}

	public static class DriverCancellationRecordExts
	{
		public static DriverCancellationRecord ToEFModels(this DriverCancellationRecordDTO source)
		{
			return new DriverCancellationRecord
			{
				Id = source.Id,
				CancellationId = source.CancellationId,
				CancellationDate = source.CancellationDate,
			};
		}
	}
}
