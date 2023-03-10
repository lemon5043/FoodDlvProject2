using FoodDlvAPI.Infrastructures;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.Infrastructures;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models.ViewModels;
using NuGet.Protocol.Core.Types;
using System.Net;

namespace FoodDlvAPI.Models.Services
{
    public class MemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> RegisterAsync(MemberRegisterDto model)
          => await _repository.CreateAsync(model);

        public async Task<MemberDTO> GetmemberAsync(int? id)
            => await _repository.GetmemberAsync(id);

        public async Task<MemberDTO> GetEditAsync(int? id)
            => await _repository.GetEditAsync(id);


        public async Task<string> EditAsync(MemberRegisterDto model)
        {

            return await _repository.EditAsync(model);
        }
        public async Task<MemberLoginresponse> Login(string account, string password)
        {
            MemberRegisterDto member = _repository.Load(account);

            if (member == null)
            {
                return MemberLoginresponse.Fail("帳密有誤");
            }

            string encryptedPW = HashUtility.ToSHA256(password, MemberEncryptedPassword.SALT);

            return string.CompareOrdinal(member.Password, encryptedPW) == 0
                ? MemberLoginresponse.Success(member.Id, member.LastName + member.FirstName, member.Password)
                : MemberLoginresponse.Fail("帳密有誤");
        }

		
		public async Task<string> GetMemberPosition(int orderId)
		{
			    //從資料庫取得起點跟終點位置
				var query = await _repository.GetMemberPosition(orderId);
                //從資料庫取得金鑰
				string token = await _repository.GetKey("GoogleMap");
				string start = query.Address;
			    string end = query.StoreAddress;
		    	//創建一個包含起點終點跟金鑰參數的request
			    WebRequest request = WebRequest.Create("https://maps.googleapis.com/maps/api/directions/json?language=zh-TW&origin=" + start + "&destination=" + end + "&key=" + token);
				//請求憑證
				request.Credentials = CredentialCache.DefaultCredentials;
		    	//發送 HTTP 請求，並獲取 API 的回應
		    	HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //從API回應得到資料
				Stream dataStream = response.GetResponseStream();
		   	   //打開資料流，以便從中讀取API的回應
		       	StreamReader reader = new StreamReader(dataStream);
		       	//讀取 API 的回應，並將其存儲在一個字符串變數中。
		       	string responseFromServer = reader.ReadToEnd();
		       	//閉資料流和 HTTP 回應，釋放資源。
		       	reader.Close();
               
		       		dataStream.Close();
		       		response.Close();
		       	//返回 Google MAP API回應，包含了從起點地址到終點地址的路線信息
		       	return responseFromServer;
		    }
		public async Task MemberLocation(MemberLocationDto location)
			=> await _repository.MemberLocation(location);

	}
}

