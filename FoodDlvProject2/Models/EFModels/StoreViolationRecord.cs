using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StoreViolationRecord
    {
        public int StoreId { get; set; }
        public int ViolationId { get; set; }
        public long OrderId { get; set; }
        public DateTime ViolationDate { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual StoreViolationType Violation { get; set; } = null!;
    }
}
