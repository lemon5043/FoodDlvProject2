using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class Store
    {
        public Store()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
            StoreCancellationRecords = new HashSet<StoreCancellationRecord>();
            StoreRatings = new HashSet<StoreRating>();
        }

        public int Id { get; set; }
        public int StorePrincipalId { get; set; }
        public string StoreName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public byte[]? Photo { get; set; }

        public virtual StorePrincipal StorePrincipal { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<StoreCancellationRecord> StoreCancellationRecords { get; set; }
        public virtual ICollection<StoreRating> StoreRatings { get; set; }
    }
}
