using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class MemberViolationType
    {
        public MemberViolationType()
        {
            MemberViolationRecords = new HashSet<MemberViolationRecord>();
        }

        public int Id { get; set; }
        public string ViolationContent { get; set; } = null!;
        public string? Content { get; set; }

        public virtual ICollection<MemberViolationRecord> MemberViolationRecords { get; set; }
    }
}
