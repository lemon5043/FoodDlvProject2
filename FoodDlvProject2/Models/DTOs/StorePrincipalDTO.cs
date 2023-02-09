using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class StorePrincipalCreateDTO
    {
        public int Id { get; set; }
        public int AccountStatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime RegistrationTime { get; set; }

    }

    public class StorePrincipalEditDTO
    {
        public int Id { get; set; }
        public int AccountStatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
    }

    public static class StorePrincialCreateExts
    {
        public static StorePrincipalCreateDTO StorePrincipalCreateToDTO(this StorePrincipalCreateVM storePrincipalCreateVM)
        {
            return new StorePrincipalCreateDTO
            {
                Id = storePrincipalCreateVM.Id,
                AccountStatusId = storePrincipalCreateVM.AccountStatusId,
                FirstName = storePrincipalCreateVM.FirstName,
                LastName = storePrincipalCreateVM.LastName,
                Phone = storePrincipalCreateVM.Phone,
                Gender = storePrincipalCreateVM.Gender,
                Email = storePrincipalCreateVM.Email,
                Birthday = storePrincipalCreateVM.Birthday,
                Account = storePrincipalCreateVM.Account,
                Password = storePrincipalCreateVM.Password,
                RegistrationTime = storePrincipalCreateVM.RegistrationTime,
            };
        }
      
        public static StorePrincipal StorePrincipalCreateDTOToStorePrincipal(this StorePrincipalCreateDTO storePrincipalCreateDTO)
        {
            return new StorePrincipal
            {
                Id = storePrincipalCreateDTO.Id,
                AccountStatusId = storePrincipalCreateDTO.AccountStatusId,
                FirstName = storePrincipalCreateDTO.FirstName,
                LastName = storePrincipalCreateDTO.LastName,
                Phone = storePrincipalCreateDTO.Phone,
                Gender = storePrincipalCreateDTO.Gender,
                Email = storePrincipalCreateDTO.Email,
                Birthday = storePrincipalCreateDTO.Birthday,
                Account = storePrincipalCreateDTO.Account,
                Password = storePrincipalCreateDTO.Password,
                RegistrationTime = storePrincipalCreateDTO.RegistrationTime,

            };
        }
        
    }



    public static class StorePrinciaEditExts 
    {
        public static StorePrincipal StorePrincipalEditDTOToStorePrincipal(this StorePrincipalEditDTO storePrincipalEditDTO)
        {
            return new StorePrincipal
            {
                Id = storePrincipalEditDTO.Id,
                AccountStatusId = storePrincipalEditDTO.AccountStatusId,
                FirstName = storePrincipalEditDTO.FirstName,
                LastName = storePrincipalEditDTO.LastName,
                Phone = storePrincipalEditDTO.Phone,
                Gender = storePrincipalEditDTO.Gender,
                Email = storePrincipalEditDTO.Email,
                Birthday=storePrincipalEditDTO.Birthday,
            };
        }


        public static StorePrincipalEditDTO StorePrincipalEditToDTO(this StorePrincipalEditVM storePrincipalEditVM)
        {
            return new StorePrincipalEditDTO
            {
                Id = storePrincipalEditVM.Id,
                AccountStatusId = storePrincipalEditVM.AccountStatusId,
                FirstName = storePrincipalEditVM.FirstName,
                LastName = storePrincipalEditVM.LastName,
                Phone = storePrincipalEditVM.Phone,
                Gender = storePrincipalEditVM.Gender,
                Birthday = storePrincipalEditVM.Birthday,
                Email = storePrincipalEditVM.Email
            };
        }
    }
}
