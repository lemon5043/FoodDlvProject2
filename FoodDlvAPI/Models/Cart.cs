﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FoodDlvAPI.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public long Id { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}