using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using Microsoft.CodeAnalysis;

namespace FoodDlvAPI.Repositories
{
    public class ProductSelectionRepository : IProductSelectionRepository
    {
        //Fields
        private readonly AppDbContext _context;

        //Constructors
        public ProductSelectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDTO> GetProductSelection(long productId)
        {
            var data = _context.Products
                .Select(p => new ProductDTO
                {
                    ProductId = p.Id,
                    ProductName = p.ProductName,
                    ProductContent = p.ProductContent,
                    StoreId = p.StoreId,
                    Photo = p.Photo,
                    Status = p.Status,
                    UnitPrice = p.UnitPrice,
                    customizationItem = p.ProductCustomizationItems
                        .Select(pci => new ProductCustomizationItemDTO
                        {
                            ProuctId = p.Id,
                            Id = pci.Id,
                            ItemName = pci.ItemName,
                            CustomizationItemPrice = pci.UnitPrice,
                        })
                })
                .Where(p => p.ProductId == productId);

            return data;
        }
    }
}
