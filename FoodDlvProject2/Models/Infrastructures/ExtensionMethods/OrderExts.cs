using FoodDlvProject2.EFModels;

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
