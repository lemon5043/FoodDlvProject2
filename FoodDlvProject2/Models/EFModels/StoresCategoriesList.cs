using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StoresCategoriesList
    {
        public int StoreId { get; set; }
        public int CategoryId { get; set; }

        public virtual StoreCategory Category { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
