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
                
        public CartDTO Load(int memberAccount)
        {
            var data = _context.Carts               
                .SingleOrDefault(c => c.MemberId == memberAccount);            

            return data.ToCartDTO();
        }
        
        public CartDTO CreateNewCart(int memberAccount)
        {
            var cart = new Cart { MemberId = memberAccount };
            _context.Carts.Add(cart);
            _context.SaveChanges();

            return Load(memberAccount);
        }
       
        public void EmptyCart(int memberAccount)
        {
            var cart = _context.Carts.SingleOrDefault(c => c.MemberId == memberAccount);
            if (cart == null) return;
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
               
        public int IdentifyNumSelector()
        {
            var selectIdentifyNum = _context.CartCustomizationItems.Select(cci => cci.IdentifyNum);
            int identifyNum;
            if (selectIdentifyNum == null)
            {
                identifyNum = 1;                
            }
            else
            {
                identifyNum = selectIdentifyNum.Max() + 1;               
            }

            return identifyNum;
        }

        public void Save(CartDTO cart, CartDetailDTO product, IEnumerable<CartCustomizationItemDTO> item) 
        {             
            var cartEntity = cart.ToCartEntity();
            _context.Carts.Add(cartEntity);
            _context.SaveChanges();

            var cartDetailEntity = product.ToCartDetailEntity();
            _context.CartDetails.Add(cartDetailEntity);
            _context.SaveChanges();

            foreach (var cartCustomizationItem in item) 
            {
                var cartCustomizationItemEntity = cartCustomizationItem.ToCartCustomizationItemEntity();
                _context.CartCustomizationItems.Add(cartCustomizationItemEntity);
            }
            _context.SaveChanges();

        }
    }
}
