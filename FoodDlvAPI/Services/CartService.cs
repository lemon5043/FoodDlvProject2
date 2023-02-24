using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
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
            if (request.Qty <= 0) throw new Exception("商品數量不可小於1");

            var cart = Current(memberAccount);

            var product = _productRepository.Load(request.ProductId, request.customizationItem.Id, true);
            var cartDetail = new CartDetailDTO(product.ProductId, request.Qty, cart.Id);
            int identifyNum = _cartRepository.IdentifyNumSelector();

            var cartCustomizationItem = product.ProductCustomizationItems
                .Select(pci => new CartCustomizationItemDTO
                (
                    pci.Id,
                    pci.ProuctId,
                    cartDetail.Id,
                    request.Qty,
                    identifyNum
                ));

            _cartRepository.Save(cart, cartDetail, cartCustomizationItem);            
        }
        

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
