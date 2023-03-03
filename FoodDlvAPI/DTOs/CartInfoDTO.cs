using FoodDlvAPI.Models;

namespace FoodDlvAPI.DTOs
{
    /// <summary>
    /// 供購物車使用的檢視資訊
    /// </summary>
    public class CartInfoDTO
    {
        public long CartId { get; set; }
        public int MemberId { get; set; }
        public int StoreId { get; set; }
        public int IdentifyNum { get; set; }
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public List<int> ItemId { get; set; }
        public string? ItemName { get; set; }       
        public int Qty { get; set; }
        public int SubTotal { get; set; }
        public int Total { get; set; }
    }    
}
