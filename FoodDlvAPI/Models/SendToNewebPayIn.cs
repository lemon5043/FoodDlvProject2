namespace FoodDlvAPI.Models
{
	public class SendToNewebPayIn
	{
		public string ChannelID { get; set; }
		//特店編號
		public string MerchantID { get; set; }
		//訂單編號
		public string MerchantOrderNo { get; set; }
		// 商品資訊
		public string ItemDesc { get; set; }
		//訂單金額
		public string Amt { get; set; }
		//繳費有效期限
		public string ExpireDate { get; set; }
		//支付完成返回網址
		public string ReturnURL { get; set; }
		//商店取號網址
		public string CustomerURL { get; set; }
		//支付通知網址
		public string NotifyURL { get; set; }
		//返回商店網址
		public string ClientBackURL { get; set; }
		public string Email { get; set; }
		//付款模式
		public string PayOption { get; set; }
	}
}
