using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class ProcessingStatue
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
    }
}
