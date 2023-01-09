using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.BackSideVM
{
	public class OrderVM
	{
		[Display(Name = "訂單編號" )]
		public long Id { get; set; }
		public int DeliveryDriversId { get; set; }

		[Display(Name = "會員編號")]
		public int MemberId { get; set; }

		[Display(Name = "商家編號")]
		public int StoreId { get; set; }
		public int DeliveryFee { get; set; }
		public string DeliveryAddress { get; set; }
	}
}
