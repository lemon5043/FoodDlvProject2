﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DriverCancellation
    {
        public DriverCancellation()
        {
            DriverCancellationRecords = new HashSet<DriverCancellationRecord>();
        }

        public int Id { get; set; }
        public string Reason { get; set; } = null!;
        public string? Content { get; set; }

        public virtual ICollection<DriverCancellationRecord> DriverCancellationRecords { get; set; }
    }
}
