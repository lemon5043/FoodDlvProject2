namespace FoodDlvProject2.Models.ViewModels
{
    internal class DeliveryDriversEditVM
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
		public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string Idcard { get; set; }
        public string VehicleRegistration { get; set; }
        public string DriverLicense { get; set; }
		public int AccountStatusId { get; internal set; }
		public int WorkStatuseId { get; internal set; }
	}
}