using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace FoodDlvProject2.Models.Repositories
{
    public class StorePrincipalsRepository :IStorePrincipalsRepository
    {
        private AppDbContext _context = new AppDbContext();


        private AppDbContext _context2 = new AppDbContext();

        public  async Task<IEnumerable<StorePrincipal>> GetAll()
        {
            return  _context.StorePrincipals.Include(s => s.AccountStatus);
        }

        public StorePrincipal FindById(int? id)
        {
            return _context.StorePrincipals.Find(id);
        }


        public StorePrincipal GetStorePrincipalByEmail(string email)
        {
            return _context.StorePrincipals.FirstOrDefault(x => x.Email == email);

        }

        public StorePrincipal GetStorePrincipalByAccount(string account)
        {
            return _context.StorePrincipals.FirstOrDefault(x => x.Account == account);
        }

        public void AddStorePrincipal(StorePrincipalCreateDTO storePrincipalCreateDTO)
        {
            var storePrincipal = storePrincipalCreateDTO.StorePrincipalCreateDTOToStorePrincipal();

            _context.StorePrincipals.Add(storePrincipal);
            _context.SaveChanges();
        }

        public void UpdateStorePrincipal(int id, StorePrincipalEditDTO storePrincipalEditDTO)
        {
            var storePrincipal=storePrincipalEditDTO.StorePrincipalEditDTOToStorePrincipal();




            storePrincipal.Account = _context2.StorePrincipals.Find(id).Account;
            storePrincipal.Password = _context2.StorePrincipals.Find(id).Password;
            storePrincipal.RegistrationTime = _context2.StorePrincipals.Find(id).RegistrationTime;

            _context.Update(storePrincipal);
            _context.SaveChanges();

        }

        public StorePrincipal GetStorePrincipalByEmail2(string email)
        {
            return _context2.StorePrincipals.FirstOrDefault(x => x.Email == email);
        }

        public void DeleteStorePrincipal(StorePrincipal storePrincipal) 
        {
            _context.StorePrincipals.Remove(storePrincipal);

            _context.SaveChanges();
        }







        //public Task SaveChangesAsync()
        //{
        //    return _context.SaveChangesAsync();
        //}







        public IEnumerable<AccountStatue> GetAccountStatues()
        {
            return _context.AccountStatues;
        }


    }
}
