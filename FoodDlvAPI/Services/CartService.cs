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
            //找尋或新增一台cart
            var cart = Current(request.RD_MemberId, request.RD_StoreId);

            //查詢該筆Prodct與其ProductCustomizationItem資料
            var product = _productRepository.Load(request.RD_ProductId, request.RD_Item, true);

            //連接product與cart到cartDetail
            var cartDetail = _cartRepository.AddCartDetail(product.ProductId, request.RD_Qty, cart.Id);

            //連接product內的ProductCustomizationItem資料到cartCustomizationItem
            //_cartRepository.AddCartCustomizationItem(cartDetail, product, request.RD_Qty);

            int identifyNum = _cartRepository.IdentifyNumSelector();
            var cartCustomizationItem = product.ProductCustomizationItems
                .Select(pci => new CartCustomizationItemDTO
                (
                    pci.Id,
                    pci.ProuctId,
                    cartDetail.Id,
                    request.RD_Qty,
                    identifyNum
                ));

            _cartRepository.Save(cartCustomizationItem);            
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
