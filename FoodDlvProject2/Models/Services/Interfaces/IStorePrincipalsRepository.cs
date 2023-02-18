using FoodDlvProject2.EFModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using FoodDlvProject2.Models.DTOs;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IStorePrincipalsRepository
    {
         Task<IEnumerable<StorePrincipal>> GetAll();


        IEnumerable<AccountStatue> GetAccountStatues();
        StorePrincipal FindById(int? id);


        StorePrincipal GetStorePrincipalByEmail(string email);
        StorePrincipal GetStorePrincipalByAccount(string account);

        void AddStorePrincipal(StorePrincipalCreateDTO storePrincipalCreateDTO);
        //public Task SaveChangesAsync();




        void UpdateStorePrincipal(int id, StorePrincipalEditDTO storePrincipalEditDTO);


        public StorePrincipal GetStorePrincipalByEmail2(string email);

        void DeleteStorePrincipal(StorePrincipal storePrincipal);



    }
}
