using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StoreCancellationRecord
    {
        public int Id { get; set; }
        public int CancellationId { get; set; }
        public long OrderId { get; set; }
        public int StoreId { get; set; }
        public DateTime CancellationDate { get; set; }

        public virtual StoreCancellationType Cancellation { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
