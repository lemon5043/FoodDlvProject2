using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models.EcPayModel;
using FoodDlvAPI.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FoodDlvAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MemberStoredValueController : Controller
	{
		private readonly ILogger<MemberStoredValueController> _logger;
		private readonly IConfiguration Config;

		public MemberStoredValueController(ILogger<MemberStoredValueController> logger)
		{
			_logger = logger;
			Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
		}
        [HttpGet]
        public ActionResult<string> Get()
        {
            return View("Index");
        }

        private ICommerce GetPayType(string option)
		{//選擇綠界
			return new ECPayService();
		}

		private string GetReturnValue(ICommerce service, SendToNewebPayIn inModel)
		{//取得回傳值
			switch (inModel.PayOption)
			{
				case "ECPay":
					return service.GetCallBack(inModel);

				default: throw new ArgumentException("No Such option");
			}
		}

		public IActionResult Index()
		{   //訂單編號
			ViewData["MerchantOrderNo"] = DateTime.Now.ToString("yyyyMMddHHmmss");
			//繳費有效期限   
			ViewData["ExpireDate"] = DateTime.Now.AddDays(3).ToString("yyyyMMdd");
			return View();
		}
		[HttpPost("SendToNewebPay")]
		public IActionResult SendToNewebPay(SendToNewebPayIn inModel)
		{
			var service = GetPayType(inModel.PayOption);

			return Json(GetReturnValue(service, inModel));
		}

		[HttpPost]
		public async Task<IActionResult> GetReturn(SendToNewebPayIn inModel)
		{
			var obj = await new ECPayService().GetQueryCallBack(inModel.MerchantOrderNo, inModel.Amt);
			return Json(obj);
		}

		/// <summary>
		/// 支付完成返回網址
		/// </summary>
		/// <returns></returns>
		public IActionResult CallbackReturn(string option)
		{
			var service = GetPayType(option);
			var result = service.GetCallbackResult(Request.Form);
			ViewData["ReceiveObj"] = result.ReceiveObj;
			ViewData["TradeInfo"] = result.TradeInfo;
			return View();
		}


		/// <summary>
		/// 商店取號網址
		/// </summary>
		/// <returns></returns>
		public IActionResult CallbackCustomer(string option)
		{
			var service = GetPayType(option);
			var result = service.GetCallbackResult(Request.Form);
			ViewData["ReceiveObj"] = result.ReceiveObj;
			ViewData["TradeInfo"] = result.TradeInfo;
			return View();
		}

	}
}
