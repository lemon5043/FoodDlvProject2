using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.EFModels
{
    public partial class DeliveryDriver
    {
        public DeliveryDriver()
        {
            DeliveryDriversRatings = new HashSet<DeliveryDriversRating>();
            DriverCancellationRecords = new HashSet<DriverCancellationRecord>();
            //DriverViolationRecords = new HashSet<DeliveryViolationRecord>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        [Display(Name = "帳號狀態")]
        public int AccountStatusId { get; set; }
        [Display(Name = "工作狀態")]
        public int WorkStatuseId { get; set; }
        [Display(Name = "名")]
        public string FirstName { get; set; } = null!; 
        [Display(Name = "姓")]
        public string LastName { get; set; } = null!;
        [Display(Name = "電話")]
        public string Phone { get; set; } = null!;
        [Display(Name = "性別")]
        public bool Gender { get ; set; }
        [Display(Name = "銀行帳號")]
        public string BankAccount { get; set; } = null!;
        [Display(Name = "身分證字號")]
        public string Idcard { get; set; } = null!;
        [Display(Name = "註冊時間")]
        public DateTime RegistrationTime { get; set; }
        [Display(Name = "行照")]
        public string VehicleRegistration { get; set; } = null!;
        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;
        [Display(Name = "帳號")]
        public string Account { get; set; } = null!;
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name = "駕照")]
        public string DriverLicense { get; set; } = null!;
        [Display(Name = "經度")]
        public string? Longitude { get; set; }
        [Display(Name = "緯度")]
        public string? Latitude { get; set; }

        public virtual AccountStatue AccountStatus { get; set; } = null!;
        public virtual DeliveryDriverWorkStatus WorkStatuse { get; set; } = null!;
        public virtual ICollection<DeliveryDriversRating> DeliveryDriversRatings { get; set; }
        public virtual ICollection<DriverCancellationRecord> DriverCancellationRecords { get; set; }
        //public virtual ICollection<DeliveryViolationRecord> DriverViolationRecords { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
