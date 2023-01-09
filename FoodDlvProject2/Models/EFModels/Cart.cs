using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class Cart
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
