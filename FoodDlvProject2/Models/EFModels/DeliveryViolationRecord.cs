using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryViolationRecord
    {
        public int DeliveryDriversId { get; set; }
        public int ViolationId { get; set; }
        public long OrderId { get; set; }
        public DateTime ViolationDate { get; set; }

        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual DeliveryViolationType Violation { get; set; } = null!;
    }
}
