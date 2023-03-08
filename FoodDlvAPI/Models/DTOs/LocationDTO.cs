namespace FoodDlvAPI.Models.DTOs
{
    public class LocationDTO
    {
        public int DriverId { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
    public static class LocationDTOExts
    {
        public static DeliveryDriver ToEFModel(this LocationDTO dto)
        {
            return new DeliveryDriver
            {
                Id = dto.DriverId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
            };
        }
    }
}
