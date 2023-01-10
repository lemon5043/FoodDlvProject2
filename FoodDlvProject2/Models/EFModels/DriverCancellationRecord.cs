using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DriverCancellationRecord
    {
        public int Id { get; set; }
        [Display(Name = "取消原因編號")]
        public int CancellationId { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name = "外送員編號")]
        public int DeliveryDriversId { get; set; }
        [Display(Name = "取消時間")]
        public DateTime CancellationDate { get; set; }

        public virtual DriverCancellation Cancellation { get; set; } = null!;
        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
