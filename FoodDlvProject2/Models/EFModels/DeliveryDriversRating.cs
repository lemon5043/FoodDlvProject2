using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryDriversRating
    {
        public int Id { get; set; }
        public int DeliveryDriversId { get; set; }
        public int MemberId { get; set; }
        public long OrderId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }

        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
