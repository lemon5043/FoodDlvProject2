using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.DTOs
{
    
    public class OrderProductDto
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }

        public OrderProductDto(long id, string productName, int unitPrice)
        {
            Id = id;
            ProductName = productName;
            UnitPrice = unitPrice;
        }
    }

    public static partial class ProductExts
    {
        public static OrderProductDto ToOrderProductEntity(this Product source)
        {
            return new OrderProductDto(source.Id, source.ProductName, source.UnitPrice);
        }
    }

}
