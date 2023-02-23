using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.ViewModels;
using NuGet.Protocol.Core.Types;

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

        public void ItemToCart(int memberAccount, CartVM request)
        {
            var cart = Current(memberAccount);

            var product = _productRepository.Load(request.ProductId, request.customizationItem.Id, true);
            var cartDetail = new CartDetailDTO(product.ProductId, request.Qty, cart.Id);
            //int identifyNum = product.IdentifyNumSelector();


            //var cartCustomizationItem = product.ProductCustomizationItems
            //    .Select(pci => new CartCustomizationItemDTO
            //    (
            //        pci.Id,
            //        pci.ProuctId,
            //        cartDetail.Id,
            //        request.Qty,
            //        identifyNum
            //    ));

            //cart.AddItem(request.Qty);
            //_cartRepository.Save(cart);
        }

        //public int IdentifyNumSelector()
        //{

        //}

        public CartDTO Current(int memberAccount)
        {
            if (_cartRepository.IsExists(memberAccount))
            {
                return _cartRepository.Load(memberAccount);
            }
            else
            {
                return _cartRepository.CreateNewCart(memberAccount);
            }
        }
    }
}
