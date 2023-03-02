﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FoodDlvAPI.Models
{
    public partial class ProductCustomizationItem
    {
        public ProductCustomizationItem()
        {
            CartDetails = new HashSet<CartDetail>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public long ProuctId { get; set; }
        public string ItemName { get; set; }
        public int UnitPrice { get; set; }

        public virtual Product Prouct { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}