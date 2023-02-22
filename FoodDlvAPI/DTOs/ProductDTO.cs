using FoodDlvAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodDlvAPI.DTOs
{
    public class ProductDTO
    {
        public long ProductId { get; set; }
        public int StoreId { get; set; }
        public string? ProductName { get; set; }
        public string? Photo { get; set; }
        public string? ProductContent { get; set; }
        public bool? Status { get; set; }
        public int UnitPrice { get; set; }
        public virtual IEnumerable<ProductCustomizationItemDTO>? CustomizationItem { get; set; }

        public ProductDTO(long productId, int stordId, string productName,
                        string photo, string productContent, bool? status, int unitPrice,
                        ICollection<ProductCustomizationItemDTO>? customizationItem)
        {
            this.ProductId = productId;
            this.StoreId = stordId;
            this.ProductName = productName;
            this.Photo = photo;
            this.ProductContent = productContent;
            this.Status = status;
            this.UnitPrice = unitPrice;
            this.CustomizationItem = customizationItem;
        }
    }

    public static partial class ProductExts
    {
        public static ProductDTO ToProductDTO(this Product source, IEnumerable<ProductCustomizationItemDTO>? customizationItem)
        {
            var toProduct = new ProductDTO
            (
                source.Id,
                source.StoreId,
                source.ProductName,
                source.Photo,
                source.ProductContent,
                source.Status,
                source.UnitPrice,
                customizationItem
            );
            return toProduct;
        }
    }
}
