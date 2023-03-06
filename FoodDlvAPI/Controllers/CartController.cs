using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Repositories;
using FoodDlvAPI.Services;
using FoodDlvAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// 商品客製化選擇頁面"加入購物車"按鈕
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AddToCart")]
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

        /// <summary>
        /// 購物車內容詳細資訊
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpGet("CartInfo")]
        public IActionResult CartInfo(int memberId, int storeId)
        {
            try
            {
                var CartData = _cartService.CartInfo(memberId, storeId).ToCartVM();
                return Json(CartData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 修改購物車內商品規格與數量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UpdateCart")]
        public IActionResult UpdateCart(CartVM request)
        {
            try
            {
                _cartService.UpdateCart(request);
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 移除購物車內一項商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RemoveDetail")]
        public IActionResult RemoveDetail(CartVM request)
        {
            try
            {
                _cartService.RemoveDetail(request);
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 清空購物車
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCart")]
        public IActionResult DeleteCart(int memberId, int storeId)
        {
            try
            {
                _cartService.DeleteCart(memberId, storeId);
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }    

        public IActionResult CheckOut()
        {

            return View();
        }

    }
}
