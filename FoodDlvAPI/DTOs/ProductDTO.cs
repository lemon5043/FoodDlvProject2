using FoodDlvAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodDlvAPI.DTOs
{
    public class ProductDTO
    {
        //Fields
        //private List<ProductCustomizationItemDTO> Items;

        //Properties
        public long ProductId { get; set; }
        public int StoreId { get; set; }
        public string? ProductName { get; set; }
        public string? Photo { get; set; }
        public string? ProductContent { get; set; }
        public bool? Status { get; set; }
        public int UnitPrice { get; set; }
        public List<ProductCustomizationItemDTO> Items { get; set; }



        public ProductDTO(long productId, int stordId, string productName,
                        string photo, string productContent, bool? status, int unitPrice,
                        List<ProductCustomizationItemDTO> items)
        {
            ProductId = productId;
            StoreId = stordId;
            ProductName = productName;
            Photo = photo;
            ProductContent = productContent;
            Status = status;
            UnitPrice = unitPrice;
            Items = items;
        }
    }

    public static partial class ProductExts
    {
        public static ProductDTO ToProductDTO(this Product source)
        {
            var productDTO = new ProductDTO
            (
                source.Id,
                source.StoreId,
                source.ProductName,
                source.Photo,
                source.ProductContent,
                source.Status,
                source.UnitPrice,
                source.ProductCustomizationItems.Select(pci => pci.ToProductCustomizationItemDTO()).ToList()
            );
            return productDTO;
        }
    }
}
