﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.EFModels
{
    public partial class Qa
    {
        public int Id { get; set; }
        
        public int CategoryId { get; set; }
        [Display(Name = "問題")]
        public string Title { get; set; }
        [Display(Name = "回答")]
        public string Answer { get; set; }

        [Display(Name = "類型")]
        public virtual Qacategory Category { get; set; }
    }
}