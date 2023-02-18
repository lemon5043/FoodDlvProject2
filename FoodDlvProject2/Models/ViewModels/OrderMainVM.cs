using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
	public class OrderMainVM
	{
		[Display(Name = "營收")]
		public int Revenue { get; set; }

		[Display(Name = "已完成訂單數量")]
		public int CompletedOrder { get; set; }

		[Display(Name = "例外狀況訂單數量")]
		public int ExceptionOrder { get; set; }

		[Display(Name = "訂單進行中")]
		public int InProcessingOrder { get; set; }
	}
}
