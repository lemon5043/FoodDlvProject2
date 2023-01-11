using FoodDlvProject2.EFModels;
using Microsoft.AspNetCore.Http.Features;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class OrderVM
    {
        //public OrderVM()
        //{
        //    orderDetails = new HashSet<OrderDetail>();
        //}

        [Display(Name = "訂單編號")]
        public long Id { get; set; }

        [Display(Name = "訂單建立日期")]
        public DateTime OrderTime { get; set; }

        [Display(Name = "會員編號")]
        public int MemberId { get; set; }

        [Display(Name = "商家編號")]
        public int StoreId { get; set; }

        [Display(Name = "訂單明細")]
        public List<OrderDetailVM> Items { get; set; }

        [Display(Name = "外送費")]
        public int DeliveryFee { get; set; }

        [Display(Name = "訂單總價")]
        public int Total => Items.Sum(x => x.SubTotal) + DeliveryFee;

        //public virtual ICollection<OrderDetail> orderDetails { get; set; }
    }

    //public static class ToOrderVM
    //{
    //	public static toOrderVM(this Models) 
    //	{

    //	}
    //}
}
