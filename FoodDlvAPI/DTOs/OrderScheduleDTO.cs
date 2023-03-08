using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    public class OrderScheduleDTO
    {
        public int Id { get; set; }
        public long OrderId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime MarkTime { get; set; }
    }

    public static partial class OrderScheduleExts
    {
        public static OrderSchedule ToOrderScheduleEF(this OrderScheduleDTO source)
        {
            var orderScheduleEF = new OrderSchedule()
            {
                Id = source.Id,
                OrderId = source.OrderId,
                StatusId = source.StatusId,
                MarkTime = source.MarkTime,
            };
            return orderScheduleEF;
        }
    }
}
