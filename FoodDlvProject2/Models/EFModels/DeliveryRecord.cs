using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryRecord
    {
        [Display(Name = "外送員編號")]
        public int DeliveryDriversId { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name = "外送距離")]
        public decimal Milage { get; set; }

        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
