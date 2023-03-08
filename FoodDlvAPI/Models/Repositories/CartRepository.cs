using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace FoodDlvAPI.Models.Repositories
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

        public void EmptyCart(int memberId, int storeId)
        {
            var cart = _context.Carts.SingleOrDefault(c => c.MemberId == memberId && c.StoreId == storeId);
            if (cart == null) return;
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public void AddDetail(CartDTO cart, CartInfoVM request)
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

            List<int?> listItemId = request.RD_ItemId;
            var invalidItemIds = listItemId.Where(itemId => _context.ProductCustomizationItems
                                        .Any(pci => pci.ProuctId == request.RD_ProductId && pci.Id == itemId) == false)
                                        .ToList();

            if (invalidItemIds.Count > 0)
            {
                throw new Exception($"客製化編號{string.Join(", ", invalidItemIds)}號不屬於該產品");
            }
            if (request.RD_ItemId.Count == 0)
            {
                listItemId.Add(null);
            }

            var details = cart.Details;
            var selectDetailItem = details.OrderBy(d => d.IdentifyNum).ThenBy(d => d.ItemsId).GroupBy(d => d.IdentifyNum).Select(gd => gd.Select(d => d.ItemId).ToList()).ToList();
            var identifyNum = details.OrderBy(d => d.IdentifyNum).GroupBy(d => d.IdentifyNum).Select(gd => gd.Key).ToList();
            List<int?> item = new List<int?>();
            int count = -1;
            foreach (var items in selectDetailItem)
            {
                count++;
                var sameDetail = items.SequenceEqual(listItemId);
                if (sameDetail)
                {
                    item = items;
                    break;
                }
            }

            if (item.Count == 0)
            {

                foreach (int? itemId in listItemId)
                {
                    var newDetail = new CartDetailDTO
                    {
                        IdentifyNum = IdentifyNumSelector(cart.Id),
                        ProductId = request.RD_ProductId,
                        ItemId = itemId,
                        Qty = request.RD_Qty,
                        CartId = cart.Id
                    };
                    _context.CartDetails.Add(newDetail.ToCartDetailEF());
                }
                _context.SaveChanges();
            }
            else
            {
                var targetDtail = details.Where(d => d.IdentifyNum == identifyNum[count]);
                foreach (var detail in targetDtail)
                {
                    detail.Qty += request.RD_Qty;

                    _context.CartDetails.Update(detail.ToCartDetailEF());
                }
                _context.SaveChanges();
            }
        }

        public int IdentifyNumSelector(long cartId)
        {
            var selectIdentifyNum = _context.CartDetails.Where(d => d.CartId == cartId).Select(cd => cd.IdentifyNum);

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

        public CartDTO GetCartInfo(CartDTO cart)
        {
            var identifyGroup = cart.Details.GroupBy(d => d.IdentifyNum);
            var cartDetail = identifyGroup.Select(gd => new CartDetailDTO
            {
                IdentifyNum = gd.Key,
                ProductId = gd.First().ProductId,
                ProductName = _context.Products.First(p => p.Id == gd.First().ProductId).ProductName,
                ItemsId = gd.Select(d => d.ItemId).ToList(),
                ItemName = string.Join(", ", _context.ProductCustomizationItems
                    .Where(pci => gd.Select(d => d.ItemId).Contains(pci.Id))
                    .Select(pci => pci.ItemName).ToList()),
                Qty = gd.First().Qty,
                SubTotal = (
                           _context.Products.Single(p => p.Id == gd.First().ProductId).UnitPrice +
                           _context.ProductCustomizationItems.Where(pci => gd.Select(d => d.ItemId).Contains(pci.Id)).Sum(pci => pci.UnitPrice)
                           ) * gd.First().Qty,
                CartId = gd.First().CartId,
            }).ToList();

            var cartInfo = new CartDTO
            {
                Id = cart.Id,
                MemberId = cart.MemberId,
                MemberName = _context.Members.Where(m => m.Id == cart.MemberId).Select(m => m.FirstName + " " + m.LastName).FirstOrDefault(),
                StoreId = cart.StoreId,
                StoreName = _context.Stores.First(s => s.Id == cart.StoreId).StoreName,
                DetailQty = cartDetail.Where(d => d.CartId == cart.Id).Sum(d => d.Qty),
                Total = cartDetail.Where(d => d.CartId == cart.Id).Sum(d => d.SubTotal),
                Details = cartDetail,
            };

            return cartInfo;
        }
        public void RemoveDetail(CartInfoVM cart)
        {
            var target = _context.CartDetails.Where(cd => cd.IdentifyNum == cart.RD_identifyNum).ToList();
            if (target.Count == 0) return;

            _context.CartDetails.RemoveRange(target);
            _context.SaveChanges();
        }
    }
}
