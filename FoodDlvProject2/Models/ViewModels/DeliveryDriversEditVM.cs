namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryDriversEditVM
    {
        public int Id { get; set; }
        public int AccountStatusId { get;  set; }
		public int WorkStatuseId { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
		public bool Gender { get; set; }
        public string BankAccount { get; set; }
        public string Idcard { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string VehicleRegistration { get; set; }
        public string DriverLicense { get; set; }


	}
}