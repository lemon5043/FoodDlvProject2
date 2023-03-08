namespace FoodDlvAPI.Models.DTOs
{
    public class StoreGetDTO
    {
        public int Id { get; set; }
        public int StorePrincipalId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Photo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double? Distance { get; set; }

        public IEnumerable<string> CategoryName { get; set; }
    }
}
