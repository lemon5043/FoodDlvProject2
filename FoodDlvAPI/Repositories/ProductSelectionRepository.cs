using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.ViewModels;
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

        public ProductDTO GetProductSelection(long productId, bool? status)
        {
            var product = _context.Products
                .SingleOrDefault(p => p.Id == productId && (status == null || p.Status == status));
            if (product == null || status == false) throw new Exception("無此商品或商品已下架");

            var customizationItem = _context.ProductCustomizationItems
                .Where(pci => pci.ProuctId == productId).ToList();              

            var productSelectionData = product.ToProductDTO(customizationItem);

            return productSelectionData;
        }
    }
}
