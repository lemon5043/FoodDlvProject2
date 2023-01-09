using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StoreBusinessHour
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        public virtual Store Store { get; set; } = null!;
    }
}
