using FoodDlvAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 提供購物車使用的商品類別與客製化資訊
    /// </summary>
    public class CartDetailDTO
    {
        //Fields
        private CartProductDTO _Product;        
        private int _Qty;        

        //Properties
        public int Id { get; set; }
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value > 0 ? value : throw new Exception("Qty需為正數"); }
        }
        public CartProductDTO Product
        {
            get { return _Product; }
            set { _Product = value != null ? value : throw new Exception("Product不可為null"); }
        }
        public int SubTotal => Product.Price * Qty;
        public List<CartCustomizationItemDTO> CustomizationItems { get; set; }

        //Constructors
        public CartDetailDTO(CartProductDTO product, int qty, List<CartCustomizationItemDTO> customizationItems)
        {
            Product = product;
            Qty = qty;
            CustomizationItems = customizationItems;
        }

        public CartDetailDTO(CartProductDTO product, int qty)
        {
            Product = product;
            Qty = qty;                    
        }
    }

    public static partial class CartDetailExts
    {
        public static CartDetailDTO ToCartDetailDTO(this CartDetail source)
        {
            var items = source.CartCustomizationItems.Select(cci => cci.ToCartCustomizationItemDTO()).ToList();
            CartProductDTO cartProduct = source.Product.ToCartProductDTO();
            var cartDetailDTO = new CartDetailDTO(cartProduct, source.Qty, items)
            {
                Id = source.Id,
            };
            return cartDetailDTO;
        }

        public static CartDetail ToCartDetailEF(this CartDetailDTO source, long cartId)
        {
            var cartDetailEF = new CartDetail
            {
                Id = source.Id,
                ProductId = source.Product.Id,
                Qty = source.Qty,
                CartId = cartId
            };
            return cartDetailEF;
        }
    }
}
