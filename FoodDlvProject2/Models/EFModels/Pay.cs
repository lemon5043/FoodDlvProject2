using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class Pay
    {
        public int DeliveryDriversId { get; set; }
        public int DeliveryCount { get; set; }
        public int TotalMilage { get; set; }
        public int ShareProfit { get; set; }
        public int TotalPay { get; set; }

        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
    }
}
