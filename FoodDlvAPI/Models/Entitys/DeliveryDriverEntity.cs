using FoodDlvAPI.Infrastructures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FoodDlvAPI.Models.Entitys
{
    public class DeliveryDriverEntity : DeliveryDriverEntityWithoutPassword
    {
        public string Password { get; set; }

        public string EncryptedPassword
        {
            get
            {
                string salt = SALT;
                string result = HashUtility.ToSHA256(this.Password, salt);
                return result;
            }
        }
    }

    public class DeliveryDriverEntityWithoutPassword
    {
        public const string SALT = "!@#$$DGTEGYT";

        public int Id { get; set; }

        public string Account { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public bool Gender { get; set; }

        public string? BankAccount { get; set; }
        [NotMapped]
        public IFormFile? Idcard { get; set; }

        public DateTime Birthday { get; set; }

        public string? Email { get; set; }
        [NotMapped]
        public IFormFile? VehicleRegistration { get; set; }
        [NotMapped]
        public IFormFile? DriverLicense { get; set; }

        public DateTime RegistrationTime { get; set; }
    }

    public static class DeliveryDriverEntityExts
    {
        public static DeliveryDriver ToEFModle(this DeliveryDriverEntity model)
        {
            return new DeliveryDriver
            {
                Id = model.Id,
                Account = model.Account,
                Password = model.EncryptedPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                BankAccount = model.BankAccount,
                RegistrationTime = model.RegistrationTime,
            };
        }
        public static DeliveryDriverEntity ToEntity(this DeliveryDriver entity)
        {
            if (entity == null) return null;

            return new DeliveryDriverEntity
            {
                Id = entity.Id,
                Account = entity.Account,
                Password = entity.Password,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Phone = entity.Phone,
                BankAccount = entity.BankAccount,
                RegistrationTime = entity.RegistrationTime,
            };
        }
    }
}