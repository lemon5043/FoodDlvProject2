using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.DTOs
{
    
    public class OrderProductEntity
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }

        public OrderProductEntity(long id, string productName, int unitPrice)
        {
            Id = id;
            ProductName = productName;
            UnitPrice = unitPrice;
        }
    }

    public static partial class ProductExts
    {
        public static OrderProductEntity ToOrderProductEntity(this Product source)
        {
            return new OrderProductEntity(source.Id, source.ProductName, source.UnitPrice);
        }
    }

}
