namespace FoodDlvProject2.Models.DTOs
{
    public class DeliveryDriverDto
    {
        public int Id { get; set; }
        public int AccountStatusId { get; set; }
        public int WorkStatuseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public string BankAccount { get; set; }
        public string Idcard { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string VehicleRegistration { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string DriverLicense { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public static class DeliveryDriverExts
    {
        public static DeliveryDriverDto ToEntity(this DeliveryDriverDto source)
        {
            return new DeliveryDriverDto
            {
                Id = source.Id,
                AccountStatusId = source.AccountStatusId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Gender = source.Gender,
                BankAccount = source.BankAccount,
                Idcard = source.Idcard,
                RegistrationTime = source.RegistrationTime,
                VehicleRegistration = source.VehicleRegistration,
                Birthday = source.Birthday,
                Email = source.Email,
                Account = source.Account,
                Password = source.Password,
                DriverLicense = source.DriverLicense,
                Longitude = source.Longitude,
                Latitude = source.Latitude,
            };
        }
    }
}
