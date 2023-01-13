namespace FoodDlvProject2.Models.ViewModels
{
    internal class DeliveryDriversDetailsVM
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string DriverName { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string AccountStatus { get; set; }
        public string WorkStatuse { get; set; }
        public int DeliveryViolationRecords { get; set; }
        public double? DriverRating { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string Idcard { get; set; }
        public string VehicleRegistration { get; set; }
        public string DriverLicense { get; set; }
    }
}