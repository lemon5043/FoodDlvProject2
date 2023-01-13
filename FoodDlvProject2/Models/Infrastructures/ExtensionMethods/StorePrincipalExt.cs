using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;

namespace FoodDlvProject2.Models.Infrastructures.ExtensionMethods
{
    public static class StorePrincipalCreateVMExt
    {


        public static StorePrincipalCreateVM ToStorePrincipalCreateVM(this StorePrincipal storePrincipal)
        {
            return new StorePrincipalCreateVM
            {
                Id = storePrincipal.Id,
                AccountStatusId = storePrincipal.AccountStatusId,
                FirstName = storePrincipal.FirstName,
                LastName = storePrincipal.LastName,
                Phone = storePrincipal.Phone,
                Gender = storePrincipal.Gender,
                Birthday = storePrincipal.Birthday,
                Email = storePrincipal.Email,
                Account = storePrincipal.Account,
                Password = storePrincipal.Password,


            };

        }





        public static StorePrincipal ToStorePrincipal(this StorePrincipalCreateVM storePrincipalVM)
        {
            return new StorePrincipal
            {
                Id = storePrincipalVM.Id,
                AccountStatusId = storePrincipalVM.AccountStatusId,
                FirstName = storePrincipalVM.FirstName,
                LastName = storePrincipalVM.LastName,
                Phone = storePrincipalVM.Phone,
                Gender = storePrincipalVM.Gender,
                Birthday = storePrincipalVM.Birthday,
                Email = storePrincipalVM.Email,
                Account = storePrincipalVM.Account,
                Password = storePrincipalVM.Password,


            };

        }



        public static StorePrincipalEditVM ToStorePrincipalEditVM(this StorePrincipal storePrincipal)
        {
            return new StorePrincipalEditVM
            {
                Id = storePrincipal.Id,
                AccountStatusId = storePrincipal.AccountStatusId,
                FirstName = storePrincipal.FirstName,
                LastName = storePrincipal.LastName,
                Phone = storePrincipal.Phone,
                Gender = storePrincipal.Gender,
                Birthday = storePrincipal.Birthday,
                Email = storePrincipal.Email,



            };

        }




        public static StorePrincipal ToStorePrincipal(this StorePrincipalEditVM storePrincipalVM)
        {
            return new StorePrincipal
            {
                Id = storePrincipalVM.Id,
                AccountStatusId = storePrincipalVM.AccountStatusId,
                FirstName = storePrincipalVM.FirstName,
                LastName = storePrincipalVM.LastName,
                Phone = storePrincipalVM.Phone,
                Gender = storePrincipalVM.Gender,
                Birthday = storePrincipalVM.Birthday,
                Email = storePrincipalVM.Email,



            };

        }



    }

}
