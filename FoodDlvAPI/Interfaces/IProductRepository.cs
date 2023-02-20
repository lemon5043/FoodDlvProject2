using FoodDlvAPI.DTOs;

namespace FoodDlvAPI.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// 回傳一筆商品
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        ProductDTO Load(long productId, bool? status);
    }
}
