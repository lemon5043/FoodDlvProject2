using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class CreditCard
    {
        public int MemberId { get; set; }
        public string BankName { get; set; } = null!;
        public string CreditCard1 { get; set; } = null!;

        public virtual Member Member { get; set; } = null!;
    }
}
