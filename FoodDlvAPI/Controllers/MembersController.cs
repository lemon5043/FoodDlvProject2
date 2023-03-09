using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.Services;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using static FoodDlvAPI.Models.Repositories.MemberRespitory;

namespace FoodDlvAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MembersController : Controller
	{
		private readonly MemberService memberservice;
		private readonly IConfiguration _configuration;

		public MembersController(IConfiguration configuration)
		{
			var db = new AppDbContext();
			IMemberRepository repository = new MemberRepository(db);
			this.memberservice = new MemberService(repository);
			this._configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<ActionResult<string>> Register([FromForm] MemberRegisterVM member)
		{

			if (ModelState.IsValid)
			{
				try
				{
					return await memberservice.RegisterAsync(member.ToMemberEditDto());
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			else
			{
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					ModelState.AddModelError(string.Empty, error.ErrorMessage);
				}
				return BadRequest(ModelState);
			}
		}
		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(MemberLoginVM model)
		{
			MemberLoginresponse response = await memberservice.Login(model.Account, model.Password);

			if (response.IsSuccess)
			{
				string token = CreateToken(response);
				return Ok(token);

			}

			ModelState.AddModelError(string.Empty, response.ErrorMessage);

			return response.ErrorMessage;
		}
		private string CreateToken(MemberLoginresponse response)
		{
			List<Claim> claims = new List<Claim>
			{
                //使用者的名字
                new Claim(ClaimTypes.Name, response.Username),
                //身分，在這裡可以是外送員、用戶、店家等等
                new Claim(ClaimTypes.Role, "Member"),
                //除此之外，Claim 還可以做非常多事情，請自己去查~
                new Claim(ClaimTypes.NameIdentifier,response.Id.ToString())
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken
				(
				claims: claims,
				expires: DateTime.Now.AddDays(7),
				signingCredentials: creds
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}
		// GET: api/Members/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MemberDetailVM>> Detail(int id)
		{
			try
			{
				var data = await memberservice.GetmemberAsync(id);
				return data.ToMemberDetailVM();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpPut("Edit/{id}")]
		public async Task<ActionResult<string>> Edit(MemberEditVM member)
		{
			if (ModelState.IsValid)
			{
				try
				{
					return await memberservice.EditAsync(member.ToMemberRegisterDto());
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					return BadRequest(ModelState);
				}
			}
			else
			{
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					ModelState.AddModelError(string.Empty, error.ErrorMessage);
				}
				return BadRequest(ModelState);
			}
		}
		//取得會員地址
		[HttpPut("GetMemberPosition")]
		public async Task GetMemberPosition(MemberLocationVM location)
		{
			try
			{
				await memberservice.MemberLocation(location.ToMemberLocationDto());
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}



		}
		public async Task SendAsync(IdentityMessage message) 
		{
			MailMessage mail= new MailMessage();
			//收信帳號
			NetworkCredential cred = new NetworkCredential("testfuen25@gmail.com", "zxcv12345==");
			mail.To.Add(message.Destination);
			mail.Subject = message.Subject;
			//寄信帳號
			mail.From = new System.Net.Mail.MailAddress("testfuen25@gmail.com");
			mail.IsBodyHtml = true;
			mail.Body = message.Body;
			//設定SMTP
			//創建客戶端,指定gmail smtp的服務器
			SmtpClient smtp = new SmtpClient("smtp.gmail.com");
			//禁用默認憑證
			smtp.UseDefaultCredentials = false;
			//啟用SSL加密
			smtp.EnableSsl = true;
			//指定smtp憑證,包含帳號密碼
			smtp.Credentials = cred;
			//端口
			smtp.Port = 587;
			//送出Mail
			await smtp.SendMailAsync(mail);

		}
		
	//[HttpPut("Edit/{id}")]
	//public async Task<ActionResult<string>> StoreValue(MemberStoreValueVM member)
	//{
	//    if (ModelState.IsValid)
	//    {
	//        try
	//        {
	//            return await ECPayService.;
	//        }
	//        catch (Exception ex)
	//        {
	//            ModelState.AddModelError(string.Empty, ex.Message);
	//            return BadRequest(ModelState);
	//        }
	//    }
	//    else
	//    {
	//        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
	//        {
	//            ModelState.AddModelError(string.Empty, error.ErrorMessage);
	//        }
	//        return BadRequest(ModelState);
	//    }
	//}

}
}
