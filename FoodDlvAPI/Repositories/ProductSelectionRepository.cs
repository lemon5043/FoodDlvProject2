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

        public IEnumerable<ProductSelectionDTO> GetProductSelection(long productId)
        {
            var data = _context.Products
                .Select(p => new ProductSelectionDTO
                {
                    ProductId = p.Id,
                    ProductName = p.ProductName,
                    ProductContent = p.ProductContent,
                    StoreId = p.StoreId,
                    //ProductPhoto = p.Photo,
                    ProductStatus = p.Status,
                    UnitPrice = p.UnitPrice,
                    customizationItem = p.OrderCustomizationItems
                        .Select(OCI => new ProductCustomizationItemDTO
                        {
                            ProuctId = OCI.Id,
                            Id = OCI.Id,
                            ItemName = OCI.ItemName,
                            CustomizationItemPrice = OCI.UnitPrice,
                        })
                })
                .Where(p => p.ProductId == productId);

            return data;
        }
    }
}
