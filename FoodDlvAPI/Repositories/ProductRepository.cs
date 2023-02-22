using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;

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
    }
}
