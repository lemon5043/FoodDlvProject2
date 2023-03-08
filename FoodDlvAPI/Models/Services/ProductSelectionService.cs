using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.Services
{
    public class ProductSelectionService
    {
        //Fields        
        private readonly IProductSelectionRepository _repository;

        //Constuctors
        public ProductSelectionService(IProductSelectionRepository repository)
        {
            _repository = repository;
        }

        public ProductDTO ProductSelection(int productId, bool? status)
        {
            var data = _repository.GetProductSelection(productId, status);

            return data;
        }
    }
}
