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


        /// <summary>
        /// 先確認會員與商店是否存在, 再確認購物車內有無該會員與商店資料
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                .Include(c => c.CartDetails.Select(cd => cd.Product))
                .Include(c => c.CartDetails.Select(cd => cd.CartCustomizationItems))
                .Single(c => c.MemberId == memberId && c.StoreId == storeId)
                .ToCartDTO();

            return data;
        }

        public CartDTO CreateNewCart(int memberId, int storeId)
        {
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

        public void AddDetail(CartDTO cart, CartProductDTO cartProduct, int qty)
        {
            if (cartProduct == null) throw new ArgumentNullException(nameof(cartProduct));
            if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));

            var detail = cart.GetDetails().ToList();

            var sameCsutomizationItems = detail
                .Where(cdD => cdD.CustomizationItems.GroupBy(cciD => new { cciD.CustomizationItemId, cciD.IdentifyNum })
                .All(group => cartProduct.CustomizationItems.Any(pciD => pciD.Id == group.Key.CustomizationItemId)))
                .ToList();

            if(sameCsutomizationItems.Any())
            {
                foreach(var detailItem in sameCsutomizationItems)
                {
                    detailItem.Qty += qty;
                }                   
            }
            else
            {
                var newDetail = new CartDetailDTO(cartProduct, qty);

                cart.add
            }        


        }

        private List<CartCustomizationItemDTO> ConvertToCartCustomizationItems (List<ProductCustomizationItemDTO> productCustomizationItems, int qty, int cartDetailId)
        {
            var cartCustomizationItems = new List<CartCustomizationItemDTO>();
            foreach (var item in productCustomizationItems)
            {
                cartCustomizationItems.Add(new CartCustomizationItemDTO
                {
                    CustomizationItemId = item.Id,
                    ProductId = item.ProuctId,
                    CartDetailId = cartDetailId,
                    Count = qty,
                    IdentifyNum = IdentifyNumSelector()
                });
            }
            return cartCustomizationItems;
        }
        //var detail = Details.SingleOrDefault(cde => cde.Product.Id == cartProduct.Id);
        //    if (detail == null)
        //    {
        //        var cartDetail = new CartDetailDTO(product, qty);
        //Details.Add(cartDetail);
        //    }
        //    else
        //    {

        //        detail.Qty += qty;
        //    }

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
                cartDetail = new CartDetailEntity(productId, qty, cartId).ToCartDetailEntity();
                _context.CartDetails.Add(cartDetail);
            }
            else
            {
                cartDetail.Qty += qty;
            }

            _context.SaveChanges();
            return cartDetail.ToCartDetailDTO();
        }

        //public void AddCartCustomizationItem(CartDetailDTO cartDetail, ProductDTO product, int qty)
        //{
        //    int identifyNum = IdentifyNumSelector();         
        //    var cartCustomizationItem = product.ProductCustomizationItems
        //        .Select(pci => new CartCustomizationItemDTO(pci.Id, pci.ProuctId, cartDetail.Id, qty, identifyNum))
        //        .ToList();

        //    foreach(var item in cartCustomizationItem)
        //    {
        //        var existingItem = _context.CartCustomizationItems
        //            .Where(cci => cci.CustomizationItemId == item.CustomizationItemId && cci.CartDetailId == item.CartDetailId)
        //            .SingleOrDefault();
        //        if (existingItem == null)
        //        {
        //            _context.CartCustomizationItems.Add(item.ToCartCustomizationItemEntity());
        //        }
        //        else
        //        {
        //            existingItem.Count += qty;
        //        }

        //        _context.SaveChanges();
        //    }           
        //}



        //public void RemoveDetail(int productId)
        //{
        //    var detail = Details.SingleOrDefault(cde => cde.Product.Id == productId);
        //    if (detail == null) return;

        //    Details.Remove(detail);
        //}

        //public void UpdateQty(int productId, int newQty)
        //{
        //    if (newQty <= 0)
        //    {
        //        RemoveDetail(productId);
        //        return;
        //    }

        //    var detail = Details.SingleOrDefault(cde => cde.Product.Id == productId);
        //    if (detail == null) return;

        //    detail.Qty = newQty;
        //}
    }
}
