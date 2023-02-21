﻿using FoodDlvAPI.Infrastructures;
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
                Gender = model.Gender,
                BankAccount = model.BankAccount,
                Birthday = model.Birthday,
                Email = model.Email,
                RegistrationTime = model.RegistrationTime,
            };
        }
    }
}