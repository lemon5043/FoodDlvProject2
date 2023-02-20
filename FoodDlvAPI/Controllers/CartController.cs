using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Repositories;
using FoodDlvAPI.Services;
using FoodDlvAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        //Fields
        private readonly AppDbContext _context;
        private CartService _cartService;

        //Constructors
        public CartController(AppDbContext context)
        {
            _context = context;
            ICartRepository repo = new CartRepository(_context);
            this._cartService = new CartService(repo);
        }

        [HttpPost]
        public IActionResult ItemToCart(CartVM request)
        {
            var data = _cartService.ItemToCart(request);

            return new EmptyResult();
        }
    }
}
