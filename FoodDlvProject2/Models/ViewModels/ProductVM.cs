using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class ProductVM
    {
        public long Id { get; set; }
        public int StoreId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public byte[] Photo { get; set; }

        [StringLength(100)]
        public string ProductContent { get; set; }

        [Required]

        public bool Status { get; set; }
        public long? CustomizationValue { get; set; }
    }
}
