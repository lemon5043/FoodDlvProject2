using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        //Fields
        private readonly AppDbContext _context;

        //Constructors
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }



        public bool IsExists(int memberId, int storeId)
        {
            var memberCheck = _context.Members.Any(m => m.Id == memberId);
            var storeCheck = _context.Stores.Any(m => m.Id == storeId);

            if (memberCheck && storeCheck)
            {
                var isExists = _context.Carts.Any(c => c.MemberId == memberId && c.StoreId == storeId);
                return isExists;
            }
            else
            {
                throw new Exception("帳號或商店不存在");
            }
        }

        public CartDTO Load(int memberId, int storeId)
        {
            var data = _context.Carts
                .AsNoTracking()
                .Single(c => c.MemberId == memberId && c.StoreId == storeId)
                .ToCartDTO();

            return data;
        }

        //public CartDTO LoadCompleteCart(int memberId, int storeId)
        //{
        //    var cart = _context.Carts.Single(c => c.MemberId == memberId && c.StoreId == storeId);
        //    var cartDetail = _context.CartDetails.Where(cd => cd.CartId == cart.Id).ToList();
        //    var cartCustomizationItem = _context.CartCustomizationItems.Where(cci => cci.CartDetailId == cartDetail.Id);

        //    return ;
        //}

        public CartDTO CreateNewCart(int memberId, int storeId)
        {
            //建立一個空的Cart
            var cart = new Cart
            {
                MemberId = memberId,
                StoreId = storeId,
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();

            return Load(memberId, storeId);
        }

        public void EmptyCart(int memberId)
        {
            var cart = _context.Carts.SingleOrDefault(c => c.MemberId == memberId);
            if (cart == null) return;
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public int IdentifyNumSelector()
        {
            var selectIdentifyNum = _context.CartCustomizationItems.Select(cci => cci.IdentifyNum);

            int identifyNum;
            if (!selectIdentifyNum.Any())
            {
                identifyNum = 1;
            }
            else
            {
                identifyNum = selectIdentifyNum.Max() + 1;
            }

            return identifyNum;
        }

        public void Save(IEnumerable<CartCustomizationItemDTO> item)
        {           

            foreach (var cartCustomizationItem in item)
            {
                var cartCustomizationItemEntity = cartCustomizationItem.ToCartCustomizationItemEntity();
                _context.CartCustomizationItems.Add(cartCustomizationItemEntity);
            }
            _context.SaveChanges();
        }

        public CartDetailDTO AddCartDetail(long productId, int qty, long cartId)
        {
            var cartDetail = _context.CartDetails
                .SingleOrDefault(cd => cd.CartId == cartId && cd.ProductId == productId);
              
            if (cartDetail == null)
            {                
                cartDetail = new CartDetailDTO(productId, qty, cartId).ToCartDetailEntity();
                _context.CartDetails.Add(cartDetail);               
            }
            else
            {                
                cartDetail.Qty += qty;
            }

            _context.SaveChanges();
            return cartDetail.ToCartDetailDTO();
        }

        public void AddCartCustomizationItem(CartDetailDTO cartDetail, ProductDTO product, int qty)
        {
            int identifyNum = IdentifyNumSelector();         
            var cartCustomizationItem = product.ProductCustomizationItems
                .Select(pci => new CartCustomizationItemDTO(pci.Id, pci.ProuctId, cartDetail.Id, qty, identifyNum))
                .ToList();
            
            foreach(var item in cartCustomizationItem)
            {
                var existingItem = _context.CartCustomizationItems
                    .Where(cci => cci.CustomizationItemId == item.CustomizationItemId && cci.CartDetailId == item.CartDetailId)
                    .SingleOrDefault();
                if (existingItem == null)
                {
                    _context.CartCustomizationItems.Add(item.ToCartCustomizationItemEntity());
                }
                else
                {
                    existingItem.Count += qty;
                }

                _context.SaveChanges();
            }           
        }
    }
}
