using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            InverseCustomizationValueNavigation = new HashSet<Product>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long Id { get; set; }
        public int StoreId { get; set; }
        public string ProductName { get; set; } = null!;
        public byte[]? Photo { get; set; }
        public string? ProductContent { get; set; }
        public bool? Status { get; set; }
        public long? CustomizationValue { get; set; }

        public virtual Product? CustomizationValueNavigation { get; set; }
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Product> InverseCustomizationValueNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
