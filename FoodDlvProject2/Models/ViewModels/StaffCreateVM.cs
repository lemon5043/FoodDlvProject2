using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class StaffCreateVM
    {

        public int Id { get; set; }

        public IFormFile Photo { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        public string EncryptedPassword { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        [Required]
        public DateTime RegistrationTime { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}
