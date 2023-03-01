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
            IEnumerable<Product> query = _context.Products                
                .Where(p => p.Id == productId)
                .Include(p => p.ProductCustomizationItems);
            
            if(status.HasValue) query = query.Where(p => p.Status == status);

            var product = query.SingleOrDefault();
            if (product == null)throw new Exception("無此商品");
            if (status.HasValue && product.Status != status) throw new Exception("商品已下架");

            var productDTO = product.ToProductDTO();
            if(itemId != null && itemId.Count > 0)
            {
                var choiceItems = product.ProductCustomizationItems.Where(pci => itemId.Contains(pci.Id)).ToList();
                productDTO.Items = choiceItems.Select(pci => pci.ToProductCustomizationItemDTO()).ToList();
            }

            return productDTO;         
        }
    }
}
