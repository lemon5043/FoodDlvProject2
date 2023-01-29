﻿using FoodDlvProject2.EFModels;
using System;
using System.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class OrderTrackingDto
    {
        public long Id { get; set; }               
        public string MemberName { get; set; }
        public string StoreName { get; set; }
        public DateTime OrderTime { get; set; }
		public int DeliveryFee { get; set; }
		public int Total { get; set; }
        public string OrderStatus { get; set; }                  
    }

    //public static partial class OrderExts
    //{
    //    public static OrderTrackingDto ToOrderDto(this Order source, string memberName, DateTime OrderTime, int total)
    //    {            
    //        return new OrderTrackingDto
    //        { 
    //            Id = source.Id,
    //            MemberName = memberName,
    //            StoreName = source.Store.StoreName,
    //            OrderTime = OrderTime,
    //            //orderSchedule = source.OrderSchedules,
    //            DeliveryAddress = source.DeliveryAddress,
    //            DeliveryFee = source.DeliveryFee,
    //            Total = total,
    //        };
    //    }
    //}
}
