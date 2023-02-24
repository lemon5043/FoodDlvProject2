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
            ICartRepository cartRepo = new CartRepository(_context);
            IProductRepository productRepo = new ProductRepository(_context);
            this._cartService = new CartService(cartRepo, productRepo);
        }

        public int MemberAccount
        {
            get
            {
                int memberId;
                int.TryParse(User.Identity.Name, out memberId);
                int testId = 2;
                return testId;
            }
        }

        [HttpPost]
        public IActionResult ItemToCart(CartVM request)
        {
            _cartService.ItemToCart(MemberAccount, request);
            return new EmptyResult();
        }

        [HttpGet]
        public IActionResult ShowCart(CartVM? request)
        {
            var CartData = _cartService.Current(MemberAccount, request.StoreId);
            return Json(CartData);
        }

        //public IActionResult UpdateCart(CartVM request)
        //{
        //    _cartService.UpdateCart(MemberAccount, request);
        //    return new EmptyResult();   
        //}


    }
}
