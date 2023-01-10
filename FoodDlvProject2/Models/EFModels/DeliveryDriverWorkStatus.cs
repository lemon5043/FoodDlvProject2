using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryDriverWorkStatus
    {
        public DeliveryDriverWorkStatus()
        {
            DeliveryDrivers = new HashSet<DeliveryDriver>();
        }

        public int Id { get; set; }
        [Display(Name = "工作狀態")]
        public string Status { get; set; } = null!;

        public virtual ICollection<DeliveryDriver> DeliveryDrivers { get; set; }
    }
}
