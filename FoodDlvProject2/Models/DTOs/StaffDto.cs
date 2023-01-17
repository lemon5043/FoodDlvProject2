namespace FoodDlvProject2.Models.DTOs
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Account { get; set; }
        public string EncryptedPassword { get; set; }
        public string Title { get; set; }
        public string Role { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
