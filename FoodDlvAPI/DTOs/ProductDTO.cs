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
        public virtual IEnumerable<ProductCustomizationItem> ProductCustomizationItems { get; set; }        

        public ProductDTO(long productId, int stordId, string productName,
                        string photo, string productContent, bool? status, int unitPrice,
                        IEnumerable<ProductCustomizationItem> productCustomizationItems)
        {
            this.ProductId = productId;
            this.StoreId = stordId;
            this.ProductName = productName;
            this.Photo = photo;
            this.ProductContent = productContent;
            this.Status = status;
            this.UnitPrice = unitPrice;
            this.ProductCustomizationItems = productCustomizationItems;
        }
    }

    public static partial class ProductExts
    {       
        public static ProductDTO ToProductDTO(this Product source, IEnumerable<ProductCustomizationItem> productCustomizationItems)
        {
            var toProductSelect = new ProductDTO
            (
                source.Id,
                source.StoreId,
                source.ProductName,
                source.Photo,
                source.ProductContent,
                source.Status,
                source.UnitPrice,
                productCustomizationItems
            );
            return toProductSelect;
        }
    }
}
