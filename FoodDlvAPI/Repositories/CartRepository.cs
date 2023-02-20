using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.ViewModels;

namespace FoodDlvAPI.Repositories
{
    public class CartRepository:ICartRepository
    {
        //Fields
        private readonly AppDbContext _context;

        //Constructors
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CartDTO> AddItemToCart(CartVM request) 
        { 
            
        }
    }
}
