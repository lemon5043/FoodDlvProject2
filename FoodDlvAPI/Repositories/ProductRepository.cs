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

        public ProductDTO Load(long productId, int itemId, bool? status)
        {
            var product = _context.Products
                .SingleOrDefault(p => p.Id == productId && (status == null || p.Status == status));
            if (product == null || status == false) throw new Exception("無此商品或商品已下架");                        
            
            var customizationItem = _context.ProductCustomizationItems
                .Where(pci => pci.Id == itemId).ToList();                
            //if (customizationItem == null) throw new Exception("無此客製化選項");

            var loadData = product.ToProductDTO(customizationItem);                                                      
           
            return loadData;            
        }
    }
}
