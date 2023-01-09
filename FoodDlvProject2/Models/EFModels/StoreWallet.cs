using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StoreWallet
    {
        public int StoreId { get; set; }
        public long OrderId { get; set; }
        public int Total { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
