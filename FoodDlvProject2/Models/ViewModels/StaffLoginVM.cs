using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    //此VM 為管理員工的帳密資訊
    public class StaffLoginVM
    {
        [Display(Name = "帳號")]
        [Required]
        public string? Account { get; set; }

        [Display(Name = "密碼")]
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
