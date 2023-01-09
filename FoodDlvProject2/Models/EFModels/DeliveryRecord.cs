using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryRecord
    {
        public int DeliveryDriversId { get; set; }
        public long OrderId { get; set; }
        public decimal Milage { get; set; }

        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
