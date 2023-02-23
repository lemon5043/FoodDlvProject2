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
                
        public ProductDTO ProductSelection(int productId, bool? status)
        {
            var data = _repository.GetProductSelection(productId, status);

            return data;
        }
    }
}
