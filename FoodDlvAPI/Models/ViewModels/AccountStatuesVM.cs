using FoodDlvAPI.EFModels;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvAPI.Models.ViewModels
{
    public class AccountStatuesVM
    {
        public int Id { get; set; }
        [Required]

        public string Status { get; set; }
    }
    public static class AccountStatusVMExts
    {
        public static AccountStatuesVM ToAccountStatuesVM(this AccountStatueDTO source)
        {
            return new AccountStatuesVM
            {
                Id = source.Id,
                Status = source.Status,
            };
        }
    }
}
