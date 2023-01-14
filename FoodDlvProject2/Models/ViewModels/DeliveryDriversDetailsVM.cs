using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryDriversDetailsVM
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string DriverName { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string AccountStatus { get; set; }
        public string WorkStatuse { get; set; }
        public int? DeliveryViolationRecords { get; set; }
        public double? DriverRating { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string Idcard { get; set; }
        public string VehicleRegistration { get; set; }
        public string DriverLicense { get; set; }
    }
    public static partial class DriversDtoExts
    {
        public static DeliveryDriversDetailsVM ToDeliveryDriversDetailsVM(this DeliveryDriverDTO source)
        {
            return new DeliveryDriversDetailsVM
            {
                Id = source.Id,
                Account = source.Account,
                DriverName = source.LastName + source.FirstName,
                Gender = source.Gender,
                Birthday = source.Birthday,
                Phone = source.Phone,
                Email = source.Email,
                BankAccount = source.BankAccount,
                AccountStatus = source.AccountStatus,
                WorkStatuse = source.WorkStatuse,
                DeliveryViolationRecords = source.DeliveryViolationRecords,
                DriverRating = source.DriverRating,
                RegistrationTime = source.RegistrationTime,
                Idcard = source.Idcard,
                VehicleRegistration = source.VehicleRegistration,
                DriverLicense = source.DriverLicense,
            };
        }
    }
}