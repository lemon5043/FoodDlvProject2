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
        private readonly AppDbContext _context;
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
            int identifyNum = IdentifyNumSelector();

            var cartCustomizationItem = product.ProductCustomizationItems
                .Select(pci => new CartCustomizationItemDTO
                (
                    pci.Id,
                    pci.ProuctId,
                    cartDetail.Id,
                    request.Qty,
                    identifyNum
                ));

            cart.AddItem(request.Qty);
            _cartRepository.Save(cart);
        }

        public int IdentifyNumSelector()
        {
            var identifyNum = _context.CartCustomizationItems.Select(cci => cci.IdentifyNum);
            if (data == null)
            {
                var identifyNum = Convert.ToInt32(data);
                identifyNum = 1;
            }
            else
            {
                var identifyNum = Convert.ToInt32(data);                
                identifyNum += 1;
            }

            return identifyNum;
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
