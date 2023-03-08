using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace FoodDlvAPI.Models.Services
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

        public void AddToCart(CartInfoVM request)
        {
            var cart = Current(request.RD_MemberId, request.RD_StoreId);
            _cartRepository.AddDetail(cart, request);
        }

        public CartDTO CartInfo(int memberId, int storeId)
        {
            var cart = Current(memberId, storeId);
            var cartInfo = _cartRepository.GetCartInfo(cart);
            return cartInfo;
        }


        public CartDTO Current(int memberId, int storeId)
        {
            if (_cartRepository.IsExists(memberId, storeId))
            {
                return _cartRepository.Load(memberId, storeId);
            }
            else
            {
                return _cartRepository.CreateNewCart(memberId, storeId);
            }
        }

        public void UpdateCart(CartInfoVM request)
        {
            _cartRepository.RemoveDetail(request);
            if (request.RD_Qty >= 1)
            {
                AddToCart(request);
            }
        }

        public void RemoveDetail(CartInfoVM request)
        {
            _cartRepository.RemoveDetail(request);
        }

        public void DeleteCart(int memberId, int storeId)
        {
            _cartRepository.EmptyCart(memberId, storeId);
        }

        public void CheckOutCart(int memberId, int storeId)
        {
            var cart = Current(memberId, storeId);
            if (cart.Details.Count == 0 || cart.Details == null)
            {
                throw new Exception("購物車內無商品, 無法進行結帳");
            }
        }


    }
}
