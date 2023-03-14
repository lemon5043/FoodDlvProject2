using FoodDlvAPI.Models.EcPayModel;

namespace FoodDlvAPI.Interfaces
{
	public interface ICommerce
	{
		string GetCallBack(SendToNewebPayIn inModel);
		//string GetPeriodCallBack(SendToNewebPayIn inModel);
		Result GetCallbackResult(IFormCollection form);
	}
}
