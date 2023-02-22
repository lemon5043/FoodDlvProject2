using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        public ProductDTO Load(long productId, bool? status)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.ProductCustomizationItems.Where(pci => pci.ProuctId == productId))
                .Where(p => p.Id == productId);
            if(status.HasValue)
            {
                query = query.Where(p => p.Status == status);
            }

            var product = query.FirstOrDefault();
            var items = product.ProductCustomizationItems.Select(p => new ProductCustomizationItemDTO()
            {

            });

            if(product == null) 
            {
                return null;
            }
            else
            {
                return product.ToProductDTO(items);
            }
        }
    }
}
