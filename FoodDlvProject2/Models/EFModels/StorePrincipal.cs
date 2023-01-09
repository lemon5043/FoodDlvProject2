using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class StorePrincipal
    {
        public StorePrincipal()
        {
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public int AccountStatusId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = null!;
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime RegistrationTime { get; set; }

        public virtual AccountStatue AccountStatus { get; set; } = null!;
        public virtual ICollection<Store> Stores { get; set; }
    }
}
