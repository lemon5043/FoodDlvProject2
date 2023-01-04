using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StoreRating
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }
        public long OrderId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
