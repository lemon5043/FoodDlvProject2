using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryViolationType
    {
        public int Id { get; set; }
        public string ViolationContent { get; set; } = null!;
        public string? Content { get; set; }
    }
}
