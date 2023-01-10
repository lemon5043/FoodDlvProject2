using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryViolationRecord
    {
        [Display(Name = "外送員編號")]
        public int DeliveryDriversId { get; set; }
        [Display(Name = "違規編號")]
        public int ViolationId { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name = "違規時間")]
        public DateTime ViolationDate { get; set; }

        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual DeliveryViolationType Violation { get; set; } = null!;
    }
}
