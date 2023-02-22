using FoodDlvAPI.DTOs;

namespace FoodDlvAPI.Interfaces
{
    public interface IProductSelectionRepository
    {
        IEnumerable<ProductSelectionDTO> GetProductSelection(long productId);

    }
}
