﻿using FoodDlvAPI.DTOs;
using FoodDlvAPI.ViewModels;

namespace FoodDlvAPI.Interfaces
{
    public interface ICartRepository
    {
        /// <summary>
        /// 回傳此顧客的購物車是否存在
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        bool IsExists(int memberId, int storeId);

        /// <summary>
        /// 建立該會員對應商店專屬的空的購物車
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        CartDTO CreateNewCart(int memberId, int storeId);

        /// <summary>
        /// 讀取目前購物車的內容
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        CartDTO Load(int memberId, int storeId);
        
        /// <summary>
        /// 清空購物車, 並刪除Database中的購物車主檔與明細檔
        /// </summary>
        /// <param name="memberId"></param>
        void EmptyCart(int memberId);

        /// <summary>
        /// 監聽"加入購物車", 觸發商品客製化識別號碼的選擇器
        /// </summary>
        /// <returns></returns>
        int IdentifyNumSelector();

        /// <summary>
        /// 儲存購物車資料CartDetail, CartCustomizationItem至Database
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="product"></param>
        /// <param name="item"></param>
        void Save(IEnumerable<CartCustomizationItemDTO> item);

        /// <summary>
        /// 添加購物車明細
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="qty"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        CartDetailDTO AddCartDetail(long productId, int qty, long cartId);

        /// <summary>
        /// 添加購物車客製化選項
        /// </summary>
        /// <param name="cartDetail"></param>
        /// <param name="product"></param>
        /// <param name="id"></param>
        /// <param name="qty"></param>
        void AddCartCustomizationItem(CartDetailDTO cartDetail, ProductDTO product, int qty);
    }
}
