﻿using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.ViewModels;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// 篩選訂單
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderDto>> SearchAsync(DateTime? start, DateTime? end, string keyWord);

        IEnumerable<OrderDetailDto> DetailSearch(long orderId);
    }
}
