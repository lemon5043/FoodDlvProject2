using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class AccountAddress
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public string Address { get; set; } = null!;

        public virtual Member Member { get; set; } = null!;
    }
}
