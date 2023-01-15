using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryDriversEditVM
    {
        public int Id { get; set; }
        public int AccountStatusId { get;  set; }
		public int WorkStatuseId { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
		public bool Gender { get; set; }
        public string BankAccount { get; set; }
        public string Idcard { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string VehicleRegistration { get; set; }
        public string DriverLicense { get; set; }
    }
    public static class DeliveryDriversEditVMExts
	{
        public static DeliveryDriversEditVM ToDeliveryDriversEditVM(this DeliveryDriverDTO source)
        {
            return new DeliveryDriversEditVM
            {
                Id = source.Id,
                AccountStatusId = source.AccountStatusId,
                WorkStatuseId = source.WorkStatuseId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Gender = source.Gender,
                BankAccount = source.BankAccount,
                Birthday = source.Birthday,             
                Email = source.Email,               
                Idcard = source.Idcard,
                VehicleRegistration = source.VehicleRegistration,
                DriverLicense = source.DriverLicense,
            };
        }
        public static DeliveryDriverEditDTO ToDeliveryDriverEditDTO(this DeliveryDriversEditVM source)
        {
            return new DeliveryDriverEditDTO
            {
                Id = source.Id,
                AccountStatusId = source.AccountStatusId,
                WorkStatuseId = source.WorkStatuseId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Gender = source.Gender,
                BankAccount = source.BankAccount,
                Birthday = source.Birthday,
                Email = source.Email,
                Idcard = source.Idcard,
                VehicleRegistration = source.VehicleRegistration,
                DriverLicense = source.DriverLicense,
            };
        }
    }
}
