﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FoodDlvAPI.Models
{
    public partial class Qacategory
    {
        public Qacategory()
        {
            Qas = new HashSet<Qa>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Displayorder { get; set; }

        public virtual ICollection<Qa> Qas { get; set; }
    }
}