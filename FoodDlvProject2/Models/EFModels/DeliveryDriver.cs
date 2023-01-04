using System;
using System.Collections.Generic;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryDriver
    {
        public DeliveryDriver()
        {
            DeliveryDriversRatings = new HashSet<DeliveryDriversRating>();
            DriverCancellationRecords = new HashSet<DriverCancellationRecord>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int AccountStatusId { get; set; }
        public int WorkStatuseId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool Gender { get; set; }
        public string BankAccount { get; set; } = null!;
        public string Idcard { get; set; } = null!;
        public DateTime RegistrationTime { get; set; }
        public string VehicleRegistration { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = null!;
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DriverLicense { get; set; } = null!;
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }

        public virtual AccountStatue AccountStatus { get; set; } = null!;
        public virtual DeliveryDriverWorkStatus WorkStatuse { get; set; } = null!;
        public virtual ICollection<DeliveryDriversRating> DeliveryDriversRatings { get; set; }
        public virtual ICollection<DriverCancellationRecord> DriverCancellationRecords { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
