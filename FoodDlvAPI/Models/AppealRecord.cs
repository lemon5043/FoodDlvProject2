﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FoodDlvAPI.Models
{
    public partial class AppealRecord
    {
        public int Id { get; set; }
        public long OrderId { get; set; }
        public DateTime CreateTime { get; set; }
        public int ComplaintId { get; set; }
        public string Content { get; set; }
        public int StatusId { get; set; }

        public virtual ComplaintType Complaint { get; set; }
        public virtual Order Order { get; set; }
        public virtual ComplaintStatus Status { get; set; }
    }
}