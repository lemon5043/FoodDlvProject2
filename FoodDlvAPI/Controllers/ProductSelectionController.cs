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
        /// 商品客製化選擇頁面
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ProductSelection(int productId, bool? status)
        {
            try
            {
                var data = _productSelectionService.ProductSelection(productId, status).ToProductSelectionVM();
                return Json(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}
