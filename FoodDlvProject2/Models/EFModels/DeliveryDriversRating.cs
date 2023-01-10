﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryDriversRating
    {
        public int Id { get; set; }
        [Display(Name = "外送員編號")]
        public int DeliveryDriversId { get; set; }
        [Display(Name = "會員編號")]
        public int MemberId { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name = "評分")]
        public int? Rating { get; set; }
        [Display(Name = "評論")]
        public string? Comment { get; set; }

        public virtual DeliveryDriver DeliveryDrivers { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
