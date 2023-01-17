using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Infrastructures.ExtensionMethods
{
    public static class StaffExtensions
    {
        public static StaffDto? ToDto(this Staff entity)
        {
            return entity == null
                ? null
                : new StaffDto
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Account = entity.Account,
                    EncryptedPassword = entity.EncryptedPassword,
                    Title = entity.Title,
                    Role = entity.Role,
                    RegistrationTime = entity.RegistrationTime,
                    Photo= entity.Photo,
                    Email= entity.Email,
                    Birthday= entity.Birthday,
                };
        }
    }
}
