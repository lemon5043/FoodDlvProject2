using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvAPI.Repositories
{
    public class CartRepository:ICartRepository
    {
        //Fields
        private readonly AppDbContext _context;

        //Constructors
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 判斷該名會員的購物車是否存在
        /// </summary>
        /// <param name="memberAccount"></param>
        /// <returns></returns>
        public bool IsExists(int memberAccount)
        {
            if(_context.Carts.SingleOrDefault(c => c.MemberId == memberAccount) != null)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        /// <summary>
        /// 讀取目前購物車的內容
        /// </summary>
        /// <param name="memberAccount"></param>
        /// <returns></returns>
        public CartDTO Load(int memberAccount)
        {
            var data = _context.Carts               
                .SingleOrDefault(c => c.MemberId == memberAccount);            

            return data.ToCartDTO();
        }

        /// <summary>
        /// 創建該會員專屬的空白購物車
        /// </summary>
        /// <param name="memberAccount"></param>
        /// <returns></returns>
        public CartDTO CreateNewCart(int memberAccount)
        {
            var cart = new Cart { MemberId = memberAccount };
            _context.Carts.Add(cart);
            _context.SaveChanges();

            return Load(memberAccount);
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="memberAccount"></param>
        public void EmptyCart(int memberAccount)
        {
            var cart = _context.Carts.SingleOrDefault(c => c.MemberId == memberAccount);
            if (cart == null) return;
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public void AddItem(CartDetailDTO product, int qty) 
        { 
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            if (qty <= 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(qty));
            }

            //var item = _context.Carts
            //    .SingleOrDefault(x => (x.ProductId == product.ProductId)
            //    && (x.CartCustomizationItems.Id == product.CustomizationItem.Id));
        }
    }
}
