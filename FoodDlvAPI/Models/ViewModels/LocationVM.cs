using FoodDlvAPI.Models.DTOs;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FoodDlvAPI.Models.ViewModels
{
    public class LocationVM
    {
        public int DriverId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
    public static class LocationVMExts
    {
        public static LocationDTO ToLocationDTO(this LocationVM VM)
            => new LocationDTO
            {
                DriverId = VM.DriverId,
                Longitude = VM.Longitude,
                Latitude = VM.Latitude,
            };
    }
}