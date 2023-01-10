

using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class AccountStatuesVM
    {
        public int Id { get; set; }
        [Required]

        public string Status { get; set; }
    }
}
