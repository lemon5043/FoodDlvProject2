using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class StorePrincipalVM
    {
         public int Id { get; set; }
        public int AccountStatusId { get; set; }
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(50)]
		public string LastName { get; set; }
		[Required]
		[StringLength(10)]
		public string Phone { get; set; }
		[Required]
	
		public bool Gender { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
		[Required]
		[StringLength(50)]
        [EmailAddress]
		public string Email { get; set; }
		[Required]
		[StringLength(50)]
		public string Account { get; set; }
		[Required]
		[StringLength(50)]
		public string Password { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
