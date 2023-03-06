namespace FoodDlvAPI.DTOs
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int IdentifyNum { get; set; }
        public long ProductId { get; set; }
        public int ProductPrice { get; set; }
        public int? ItemId { get; set; }
        public int ItemPrice { get; set; }
        public int Qty { get; set; }
        public long OrderId { get; set; }
    }
}
