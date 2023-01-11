namespace FoodDlvProject2.Models.DTOs
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public int Permissions { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
