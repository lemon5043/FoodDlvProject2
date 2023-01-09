using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DriverCancellationRecord
    {
        public int Id { get; set; }
        public int CancellationId { get; set; }
        public long OrderId { get; set; }
        public int DeliveryDriversId { get; set; }
        public DateTime CancellationDate { get; set; }

        public virtual DriverCancellation Cancellation { get; set; } = null!;
        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
