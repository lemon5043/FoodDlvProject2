using FoodDlvAPI.DTOs;

namespace FoodDlvAPI.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// 回傳一筆商品與客製化資料
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        ProductDTO Load(long productId, List<int>? itemId, bool? status);
    }
}
