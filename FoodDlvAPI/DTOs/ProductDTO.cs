namespace FoodDlvAPI.DTOs
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public int StoreId { get; set; }
        public string ProductName { get; set; }
        public byte[] Photo { get; set; }
        public string ProductContent { get; set; }
        public bool? Status { get; set; }
        public int UnitPrice { get; set; }
    }
}
