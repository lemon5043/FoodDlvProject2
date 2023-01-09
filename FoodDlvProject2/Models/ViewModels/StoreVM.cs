using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class StoreVM
    {
        public int Id { get; set; }
        public int StorePrincipalId { get; set; }
        [Required]
        [StringLength(50)]
        public string StoreName { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        public string ContactNumber { get; set; }
        public byte[] Photo { get; set; }
    }
}
