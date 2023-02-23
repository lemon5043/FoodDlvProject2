using FoodDlvAPI.DTOs;

namespace FoodDlvAPI.Interfaces
{
    public interface IProductSelectionRepository
    {
        /// <summary>
        /// 取得指定商品的內容與客製化選項
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductDTO GetProductSelection(long productId, bool? status);
    }
}
