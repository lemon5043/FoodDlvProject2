using FoodDlvProject2.Models.EFModels;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.BackSideVM
{
	public class OrderDetailVM
	{
		[Display(Name = "商品編號")]
		public long ProductId { get; set; }

		[Display(Name = "商品名稱")]
		public string ProductName { get; set; }

		[Display(Name = "商品單價")]
		public int UnitPrice { get; set; }

		[Display(Name = "商品數量")]
		public int Count { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}
