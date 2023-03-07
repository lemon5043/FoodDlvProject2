using FoodDlvProject2.EFModels;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDlvProject2.Models.DTOs
{

    public class DeliveryDriverEditDTO
    {
        public int Id { get; set; }
        public int AccountStatusId { get; set; }
        public int WorkStatuseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public string BankAccount { get; set; }
        [NotMapped]
        public IFormFile Idcard { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public IFormFile VehicleRegistration { get; set; }
        [NotMapped]
        public IFormFile DriverLicense { get; set; }
    }
    public static class DeliveryDriverEditDTOExts
    {
        public static DeliveryDriver ToEFModle(this DeliveryDriverEditDTO model)
        {
            return new DeliveryDriver
            {
                Id = model.Id,
                AccountStatusId = model.AccountStatusId,
                WorkStatuseId = model.WorkStatuseId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                BankAccount = model.BankAccount,
            };
        }
    }

}
