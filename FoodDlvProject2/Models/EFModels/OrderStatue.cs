using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class OrderStatue
    {
        public OrderStatue()
        {
            OrderSchedules = new HashSet<OrderSchedule>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<OrderSchedule> OrderSchedules { get; set; }
    }
}
