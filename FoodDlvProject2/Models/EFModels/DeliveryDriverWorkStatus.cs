using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryDriverWorkStatus
    {
        public DeliveryDriverWorkStatus()
        {
            DeliveryDrivers = new HashSet<DeliveryDriver>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<DeliveryDriver> DeliveryDrivers { get; set; }
    }
}
