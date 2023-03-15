//using FoodDlvAPI.Interfaces;
//using FoodDlvAPI.Models.EcPayModel;
//using System.Collections.Specialized;
//using System.Net;
//using System.Text;
//using System.Web;

//namespace FoodDlvAPI.Models.Services
//{
//	public class ECPayService : ICommerce
//	{
//		public IConfiguration Config { get; set; }

//		public ECPayService()
//		{
//			Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
//		}

//		public string GetCallBack(SendToNewebPayIn inModel)
//		{
//			var orderId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

//			//需填入 你的網址
//			var website = $"{Config.GetSection("https://localhost:7093").Value}/MemberStoredValueController";

//			var order = new Dictionary<string, object>
//			{
//                //特店交易編號
               
//			};

//			//檢查碼
//			order["CheckMacValue"] = GetCheckMacValue(order);

//			StringBuilder s = new StringBuilder();
//			s.AppendFormat("<form id='payForm' action='{0}' method='post'>", "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5");
//			foreach (KeyValuePair<string, object> item in order)
//			{
//				s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", item.Key, item.Value);
//			}

//			s.Append("</form>");

//			return s.ToString();
//		}

//		//public string GetPeriodCallBack(SendToNewebPayIn inModel)
//		//{
//		//	var orderId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

//		//	//需填入 你的網址
//		//	var website = $"{Config.GetSection("HostURL").Value}/Home";

//		//	var order = new Dictionary<string, object>
//		//	{
//  //              //選擇預設付款方式 固定Credit
//  //              { "ChoosePayment",  "Credit"},

//  //              //交易金額
//  //              { "PeriodAmount",  int.Parse(inModel.Amt)},
                
//  //              ////自訂名稱欄位2
//  //              //{ "PeriodType ",  "D"},

//  //              ////自訂名稱欄位2
//  //              //{ "Frequency",  1},

//  //              ////自訂名稱欄位2
//  //              //{ "ExecTimes",  5},
                
//  //              //完成後發通知
//  //              { "PeriodReturnURL",  $"{Config.GetSection("HostURL").Value}/Home/CallbackNotify?option=ECPay"},                

//  //              //自訂名稱欄位1
//  //              { "Email",  inModel.Email},
                
//  //             //完成後發通知
//  //              { "ReturnURL",  $"{Config.GetSection("HostURL").Value}/Notify/CallbackNotify?option=ECPay"},

//  //              //付款完成後導頁
//  //              { "OrderResultURL", $"{Config.GetSection("HostURL").Value}/Home/CallbackReturn?option=ECPay"},


//  //              ////付款方式為 ATM 時，當使用者於綠界操作結束時，綠界回傳 虛擬帳號資訊至 此URL
//  //              //{ "PaymentInfoURL",$"{Config.GetSection("HostURL").Value}/Home/CallbackCustomer?option=ECPay"},

//  //              ////付款方式為 ATM 時，當使用者於綠界操作結束時，綠界會轉址至 此URL。
//  //              //{ "ClientRedirectURL",  $"{Config.GetSection("HostURL").Value}/Home/CallbackCustomer?option=ECPay"},

//  //              //特店編號， 2000132 測試綠界編號
//  //              { "MerchantID",  "2000132"},

//  //              //交易類型 固定填入 aio
//  //              { "PaymentType",  "aio"},


//  //              //CheckMacValue 加密類型 固定填入 1 (SHA256)
//  //              { "EncryptType",  "1"},
//		//	};

//		//	
//		//}

		

//		private string GetCheckMacValue(Dictionary<string, string> order)
//		{
//			var param = order.Keys.OrderBy(x => x).Select(key => key + "=" + order[key]).ToList();

//			var checkValue = string.Join("&", param);

//			//測試用的 HashKey
//			var hashKey = "dALAm7IGaqMq8ebH";

//			//測試用的 HashIV
//			var HashIV = "ZV1cJFulEf25mRCG";

//			checkValue = $"HashKey={hashKey}" + "&" + checkValue + $"&HashIV={HashIV}";

//			checkValue = HttpUtility.UrlEncode(checkValue).ToLower();

//			checkValue = EncryptSHA256(checkValue);

//			return checkValue.ToUpper();
//		}

//		/// <summary>
//		/// 支付通知網址
//		/// </summary>
//		/// <returns></returns>
//		public Result GetCallbackResult(IFormCollection form)
//		{
//			// 接收參數
//			StringBuilder receive = new StringBuilder();
//			foreach (var item in form)
//			{
//				receive.AppendLine(item.Key + "=" + item.Value + "<br>");
//			}
//			var result = new Result
//			{
//				ReceiveObj = receive.ToString(),
//			};

//			// 解密訊息
//			IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
//			string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
//			string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
//			string TradeInfoDecrypt = DecryptAESHex(form["TradeInfo"], HashKey, HashIV);
//			NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
//			receive.Length = 0;
//			foreach (String key in decryptTradeCollection.AllKeys)
//			{
//				receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
//			}
//			result.TradeInfo = receive.ToString();

//			return result;
//		}

//		public async Task<NewebPayReturn<NewebPayQueryResult>> GetQueryCallBack(string orderId, string amt)
//		{
//			var dict = new Dictionary<string, string>
//			{
//				{ "MerchantID", "1039919" },
//				{ "MerchantTradeNo", "141871950171249" },
//				{ "TimeStamp", DateTime.Now.ToString() }
//			};

//			dict.Add("CheckMacValue", GetCheckMacValue(dict));

//			var result = GetApiInvokeResult("https://payment.ecpay.com.tw/Cashier/QueryTradeInfo/V5", string.Join("&", dict.Select(a => a.Key + "=" + a.Value)), contentType: "application/x-www-form-urlencoded");

//			return null;
//		}

//		public NewebPayReturn<NewebPayQueryResult> GetApiInvokeResult(string url, string postData = null, string contentType = null)
//		{

//			var request = (HttpWebRequest)WebRequest.Create(url);

//			if (postData == null) throw new ArgumentNullException(nameof(postData));
//			var paramBytes = Encoding.UTF8.GetBytes(postData);

//			request.Method = "POST";
//			request.ContentType = string.IsNullOrWhiteSpace(contentType) ? "application/x-www-form-urlencoded" : contentType;
//			request.ContentLength = paramBytes.Length;
//			using (var stream = request.GetRequestStream())
//			{
//				stream.Write(paramBytes, 0, paramBytes.Length);
//			}
//			var response = (HttpWebResponse)request.GetResponse();
//			string a;
//			using (var sr = new StreamReader(response.GetResponseStream()))
//			{
//				a = sr.ReadToEnd();
//			}

//			return null;
//		}

		

		

		

		

		

//	}
//}
