using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDlvAPI.Models.Services;
using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FoodDlvAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize(Roles = "DeliveryDriver")]
	public class DeliveryDriversController : Controller
    {
        private readonly DeliveryDriverService deliveryDriverService;
        
        private readonly IConfiguration _configuration;

        public DeliveryDriversController(IConfiguration configuration)
        {
            var db = new AppDbContext();
            IDeliveryDriversRepository repository = new DeliveryDriversRepository(db);
            this.deliveryDriverService = new DeliveryDriverService(repository);
            this._configuration= configuration;
        }

		[AllowAnonymous]
		[HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginVM model)
        {
            LoginResponse response = await deliveryDriverService.Login(model.Account, model.Password);

            if (response.IsSuccess)
            {
                string token = CreateToken(response);
                return Ok(token);

                //todo?
                //登入後除了 JWT access token，也生成一個 refresh token
                //var refreshToken = GenerateRefreshToken();
                //SetRefreshToken(refreshToken);
            }

            ModelState.AddModelError(string.Empty, response.ErrorMessage);

            return response.ErrorMessage;
        }

		//這是伯翰寫ㄉ，不確定前台會不會用到，以防萬一先寫
		/// <summary>
		/// 獲得透過 token 得到的用戶 ID、名字和身分
		/// </summary>
		/// <returns></returns>
        [HttpGet]
		public ActionResult<object> GetDriver()
		{
			var driverId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var driverAccount = User.FindFirstValue(ClaimTypes.Name);
			var role = User.FindFirstValue(ClaimTypes.Role);
			return Ok(new { driverId, driverAccount, role });

		}

		[AllowAnonymous]
		private string CreateToken(LoginResponse response)
        {
            //Claim 的中文叫做宣告，代表主體的屬性
            List<Claim> claims = new List<Claim>
            {
                //使用者的名字
                new Claim(ClaimTypes.Name, response.Username),
                //身分，在這裡可以是外送員、用戶、店家等等
                new Claim(ClaimTypes.Role, "DeliveryDriver"),
                //除此之外，Claim 還可以做非常多事情，請自己去查~
                new Claim(ClaimTypes.NameIdentifier,response.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> LogOut()
        //{

        //    // Clear the existing external cookie
        //    await HttpContext.SignOutAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme);

        //    var url = LocalRedirect("/Staffs/Login");
        //    return url;
        //}

        // GET: api/DeliveryDrivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryDriversDetailsVM>> Details(int id)
        {
            try
            {
                var data = await deliveryDriverService.GetOneAsync(id);
                return data.ToDeliveryDriversDetailsVM();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("register")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([FromForm]DeliveryDriverCreateVM deliveryDriver)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await deliveryDriverService.RegisterAsync(deliveryDriver.ToDeliveryDriverEditDTO());
                    return Ok();
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



        // PUT: api/DeliveryDrivers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("Edit/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DeliveryDriversEditVM deliveryDriver)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await deliveryDriverService.EditAsync(deliveryDriver.ToDeliveryDriverEntity());
                    return Ok();
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
    }
}
