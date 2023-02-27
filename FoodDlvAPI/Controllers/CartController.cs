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
                

        [HttpPost]
        public IActionResult AddToCart(CartVM request)
        {
            try
            {
                _cartService.AddToCart(request);
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet]
        public IActionResult ShowCart(int memberId, int storeId)
        {
            try
            {
                var CartData = _cartService.Current(memberId, storeId);
                return Json(CartData);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        //public IActionResult UpdateCart(CartVM request)
        //{
        //    _cartService.UpdateCart(MemberAccount, request);
        //    return new EmptyResult();   
        //}


    }
}
