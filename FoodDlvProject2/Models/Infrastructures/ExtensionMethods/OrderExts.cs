using FoodDlvProject2.Models.EFModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodDlvProject2.Models.Infrastructures.ExtensionMethods
{
    public static class OrderExts
    {
        public static DateTime ToOrderTime(this OrderSchedule orderSchedule)
        {
            return orderSchedule.MarkTime;
            //var orderTime = new OrderSchedule()
            //{
            //    Id = orderSchedule.Id,
            //    OrderId = orderSchedule.OrderId,
            //    StatusId = orderSchedule.StatusId,
            //    MarkTime = orderSchedule.MarkTime,
            //};

            //return MarkTime = orderSchedule.MarkTime;
        }
    }
}
