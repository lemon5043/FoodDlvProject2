using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;

namespace FoodDlvAPI.Services
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
                
        public IEnumerable<ProductDTO> ProductSelection(int productId)
        {
            var data = _repository.GetProductSelection(productId);

            return data;
        }
    }
}
