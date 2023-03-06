using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;

namespace FoodDlvAPI.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        //Fields
        private readonly AppDbContext _context;

        //Constructors
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

    }
}
