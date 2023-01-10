using FoodDlvProject2.Models.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FoodDlvProject2.Models.BackSideVM
{
	public class OrderVM
	{
		[Display(Name = "訂單編號" )]
		public long Id { get; set; }

		[Display(Name = "訂單建立日期")]
		public DateTime OrderTime { get; set; }

		[Display(Name = "會員編號")]
		public int MemberId { get; set; }

		[Display(Name = "商家編號")]
		public int StoreId { get; set; }

        //[Display(Name = "訂單明細")]
        //public List<OrderDetailVM> orderDetails { get; set; }

        [Display(Name = "訂單總價")]
		public int FinalTotal { get; set; }

        //public virtual ICollection<OrderDetail> orderDetails { get; set; }
    }

	//public static class ToOrderVM
	//{
	//	public static toOrderVM(this Models) 
	//	{

	//	}
	//}
}
