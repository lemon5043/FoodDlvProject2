using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Repositories;
using FoodDlvAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSelectionController : Controller
    {
        //Fields
        private readonly AppDbContext _context;
        private ProductSelectionService _productSelectionService;

        //Constructors
        public ProductSelectionController(AppDbContext context)
        {
            _context = context;
            IProductSelectionRepository repo = new ProductSelectionRepository(_context);
            this._productSelectionService = new ProductSelectionService(repo);
        }

        /// <summary>
        /// 商品購買介面
        /// </summary>
        /// <param name="productId">搜尋條件-產品Id</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ProductSelection(int productId, bool? status)
        {
            var data = _productSelectionService.ProductSelection(productId, status);

            return Json(data);
        }
    }
}
