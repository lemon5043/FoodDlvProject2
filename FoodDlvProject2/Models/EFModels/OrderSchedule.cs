using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class OrderSchedule
    {
        public int Id { get; set; }
        public long OrderId { get; set; }
        public int StatusId { get; set; }
        public DateTime MarkTime { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual OrderStatue Status { get; set; } = null!;
    }
}
