using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StoreCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CategoryContent { get; set; } = null!;
    }
}
