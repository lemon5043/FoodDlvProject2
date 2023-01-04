using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class MemberViolationRecord
    {
        public int MemberId { get; set; }
        public int ViolationId { get; set; }
        public long OrderId { get; set; }
        public DateTime ViolationDate { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual MemberViolationType Violation { get; set; } = null!;
    }
}
