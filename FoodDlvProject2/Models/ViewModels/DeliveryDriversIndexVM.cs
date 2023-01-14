using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryDriversIndexVM
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public bool Gender { get; set; }
        public string AccountStatus { get; set; }
        public string WorkStatuse { get; set; }
    }

    public static partial class DriversDtoExts
    {
        public static DeliveryDriversIndexVM ToDeliveryDriversIndexVM(this DeliveryDriverDTO source)
        {
            return new DeliveryDriversIndexVM
            {
                Id = source.Id,
                DriverName = source.LastName + source.FirstName,
                Gender = source.Gender,
                AccountStatus = source.AccountStatus,
                WorkStatuse = source.WorkStatuse,
            };
        }
    }
}