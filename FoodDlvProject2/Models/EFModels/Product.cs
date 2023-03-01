﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.EFModels
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long Id { get; set; }
        [Display(Name = "店家")]
        public int StoreId { get; set; }
        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }
		[Display(Name = "圖片")]
		public string? Photo { get; set; }
        [Display(Name = "商品內容")]
        public string? ProductContent { get; set; }
        [Display(Name = "狀態")]
        public bool? Status { get; set; }
        [Display(Name = "單價")]
        public int UnitPrice { get; set; }

        [Display(Name = "店家")]
        public virtual Store Store { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}