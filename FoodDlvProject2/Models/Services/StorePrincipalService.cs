using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Infrastructures.ExtensionMethods;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.Win32;

namespace FoodDlvProject2.Models.Services
{
    public class StorePrincipalService
    {
        private IStorePrincipalsRepository repository;

        public StorePrincipalService(IStorePrincipalsRepository repo)
        {
            repository = repo;
        }

        public StorePrincipal Find(int id) 
        {

            StorePrincipal storePrincipal = repository.FindById(id);
            if (storePrincipal == null)
            {
                throw new Exception("找不到指定的記錄");
            }
            return storePrincipal;
        }

        public void CreateStorePrincipal(StorePrincipalCreateDTO storePrincipalCreateDTO)
        {

            //StorePrincipal storePrincipal = storePrincipalCreateDTO.ToStorePrincipal();

            var emailExist = repository.GetStorePrincipalByEmail(storePrincipalCreateDTO.Email);
            var accountExist = repository.GetStorePrincipalByAccount(storePrincipalCreateDTO.Account);

            if (emailExist != null)
            {
                throw new Exception("Email已經報名過了, 無法再度報名");
            }
            if (accountExist != null)
            {
                throw new Exception("帳號已經報名過了, 請更改帳號註冊");
            }
            storePrincipalCreateDTO.RegistrationTime = DateTime.Now;
            repository.AddStorePrincipal(storePrincipalCreateDTO);
        }

        public void EditStorePrincipal(int id, StorePrincipalEditDTO storePrincipalEditDTO) 
        {

            AppDbContext _context2 = new AppDbContext();
            var emailExist = repository.GetStorePrincipalByEmail2(storePrincipalEditDTO.Email);
            if (emailExist != null) // 表示資料表有這筆記錄
            {
                if (storePrincipalEditDTO.Email != _context2.StorePrincipals.Find(id).Email)
                {
                    throw new Exception("Email已經報名過了,請更改");
                }
            }
            repository.UpdateStorePrincipal(id, storePrincipalEditDTO);
        }
    }
}
