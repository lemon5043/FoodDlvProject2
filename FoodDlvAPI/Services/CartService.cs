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

        public void AddToCart(CartVM request)
        {               
            var cart = Current(request.RD_MemberId, request.RD_StoreId);                 
            _cartRepository.AddDetail(cart, request);
                        
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

        //public void UpdateCart(int memberAccount, CartVM request) 
        //{ 
        //    if(request.Qty <= 0)
        //    {
        //        request.Qty = 0;
        //    }
        //    else
        //    {
        //        request.Qty = request.Qty;
        //    }            
        //    var cart = Current(memberAccount);           
            
        //}
    }
}
