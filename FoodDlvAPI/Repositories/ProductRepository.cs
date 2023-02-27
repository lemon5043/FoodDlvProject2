using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FoodDlvAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //Fields
        private readonly AppDbContext _context;

        //Constructors
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public ProductDTO Load(long productId, List<int>? itemId, bool? status)
        {
            var product = _context.Products
                .SingleOrDefault(p => p.Id == productId && (status == null || p.Status == status));
            if (product == null || status == false) throw new Exception("無此商品或商品已下架");                        
            
            var customizationItem = _context.ProductCustomizationItems
                .Where(pci => itemId.Contains(pci.Id)).ToList();              

            var loadData = product.ToProductDTO(customizationItem);                                                      
           
            return loadData;            
        }
    }
}
