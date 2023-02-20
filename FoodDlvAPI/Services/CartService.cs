using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.ViewModels;

namespace FoodDlvAPI.Services
{
    public class CartService
    {
        //Fields
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;


        //Constructors
        public CartService(ICartRepository cartRepository,
                            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public void ItemToCart(CartVM request)
        {
            var cart = Current(request.MemberId);
            
            var product = _productRepository.Load(request.ProductId, true);
            var cartPord = new CartProductDTO
        }

        public CartDTO Current(int memberId)
        {
            if (_cartRepository.IsExists(memberId))
            {
                return _cartRepository.Load(memberId);
            }
            else
            {
                return _cartRepository.CreateNewCart(memberId);
            }
        }
    }
}
