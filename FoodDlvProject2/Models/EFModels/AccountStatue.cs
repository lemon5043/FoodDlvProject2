using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class AccountStatue
    {
        public AccountStatue()
        {
            DeliveryDrivers = new HashSet<DeliveryDriver>();
            Members = new HashSet<Member>();
            StorePrincipals = new HashSet<StorePrincipal>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<DeliveryDriver> DeliveryDrivers { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<StorePrincipal> StorePrincipals { get; set; }
    }
}
