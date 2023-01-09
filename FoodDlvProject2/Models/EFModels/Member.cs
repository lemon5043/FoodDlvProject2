﻿using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class Member
    {
        public Member()
        {
            DeliveryDriversRatings = new HashSet<DeliveryDriversRating>();
            Orders = new HashSet<Order>();
            StoreRatings = new HashSet<StoreRating>();
        }

        public int Id { get; set; }
        public int AccountStatusId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = null!;
        public int Balance { get; set; }
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime RegistrationTime { get; set; }

        public virtual AccountStatue AccountStatus { get; set; } = null!;
        public virtual Cart? Cart { get; set; }
        public virtual MemberViolationRecord? MemberViolationRecord { get; set; }
        public virtual ICollection<DeliveryDriversRating> DeliveryDriversRatings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StoreRating> StoreRatings { get; set; }
    }
}
