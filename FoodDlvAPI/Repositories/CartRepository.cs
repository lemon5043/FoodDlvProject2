using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Xml;

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
            var storeCheck = _context.Stores.Any(s => s.Id == storeId);

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
                .Include(c => c.CartDetails)                
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

        public void AddDetail(CartDTO cart, CartVM request)
        {         
            if (_context.Stores.Any(m => m.Id == request.RD_StoreId) == false)
            {
                throw new Exception("此商店不存在");
            }                 
            if (_context.Products.Any(p => p.StoreId == request.RD_StoreId && p.Id == request.RD_ProductId) == false)
            {
                throw new Exception("商店無此商品");
            }                
            if (request.RD_Qty <= 0)
            {
                throw new Exception("商品數量不可小於0");
            }               

            var detail = cart.GetDetails().ToList();         

            var sameDetail = detail.Select(detail => detail.ItemId).ToList().SequenceEqual(request.RD_Item);
            var identifyNum = IdentifyNumSelector();

            if (sameDetail == true)
            {
                foreach(var item in detail)
                {
                    item.Qty += request.RD_Qty;                    
                    _context.CartDetails.Update(item.ToCartDetailEF());
                }
                _context.SaveChanges();
            }
            else
            {   
                foreach (var item in request.RD_Item)
                {
                    var newDetail = new CartDetailDTO
                    (
                        identifyNum,
                        request.RD_ProductId,
                        item,
                        request.RD_Qty,
                        cart.Id
                    );                    
                    _context.CartDetails.Add(newDetail.ToCartDetailEF());
                }
                _context.SaveChanges();
            }        
        }
                
        public int IdentifyNumSelector()
        {
            var selectIdentifyNum = _context.CartDetails.Select(cd => cd.IdentifyNum);

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

        public void Save()
        {            
            throw new NotImplementedException();
        }

        public CartDTO GetCartInfo(CartDTO cart)
        {

        }




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
