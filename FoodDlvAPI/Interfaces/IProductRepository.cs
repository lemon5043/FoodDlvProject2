using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Interfaces
{
    public interface IProductRepository
    {       
        /// <summary>
        /// 回傳特定商品與被選擇的客製化資料
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        ProductDTO Load(long productId, List<int>? itemId, bool? status);
    }
}
