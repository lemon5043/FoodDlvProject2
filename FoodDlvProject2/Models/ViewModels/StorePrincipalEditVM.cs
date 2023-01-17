using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class StorePrincipalEditVM
    {
        public int Id { get; set; }

        [Display(Name = "帳號狀態")]
        public int AccountStatusId { get; set; }

        [Display(Name = "名字")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "姓氏")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "電話")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(10)]
        public string Phone { get; set; }

        [Display(Name = "性別")]
        [Required]

        public bool Gender { get; set; }

        [Display(Name = "生日")]
        [Required(ErrorMessage = "生日必填")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }


    }
    
}
