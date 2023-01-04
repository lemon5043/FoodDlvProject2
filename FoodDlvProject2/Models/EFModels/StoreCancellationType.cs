using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StoreCancellationType
    {
        public StoreCancellationType()
        {
            StoreCancellationRecords = new HashSet<StoreCancellationRecord>();
        }

        public int Id { get; set; }
        public string Reason { get; set; } = null!;
        public string? Content { get; set; }

        public virtual ICollection<StoreCancellationRecord> StoreCancellationRecords { get; set; }
    }
}
