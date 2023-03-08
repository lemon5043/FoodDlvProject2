using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDlvAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FoodDlvAPI.ViewModels;
using FoodDlvAPI.DTOs;
using NuGet.Common;
using NuGet.Protocol.Core.Types;

namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorePrincipalsController : ControllerBase
    {
        private readonly AppDbContext _context;
		private readonly IConfiguration _configuration;

		public StorePrincipalsController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(StoreLoginVM model)
		{
			StoreLoginresponse response = await Login(model.Account, model.Password);


			if (response.IsSuccess)
			{
				string token = CreateToken(response);
				return Ok(token);

			}

			ModelState.AddModelError(string.Empty, response.ErrorMessage);

			return response.ErrorMessage;
		}
		private async Task<StoreLoginresponse> Login(string account, string password)
        {
            StorePrincipal storePrincipal = await _context.StorePrincipals.Where(x => x.Account == account && x.Password == password).SingleOrDefaultAsync();

			if (storePrincipal == null)
            {
                return StoreLoginresponse.Fail("帳密有誤");
            }
            return StoreLoginresponse.Success(storePrincipal.Id,storePrincipal.FirstName+storePrincipal.LastName);
		}
		private string CreateToken(StoreLoginresponse response)
		{
			List<Claim> claims = new List<Claim>
			{
                //使用者的名字
                new Claim(ClaimTypes.Name, response.Username),
                //身分，在這裡可以是外送員、用戶、店家等等
                new Claim(ClaimTypes.Role, "Store"),
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








		// GET: api/StorePrincipals
		[HttpGet]
        public async Task<ActionResult<IEnumerable<StorePrincipal>>> GetStorePrincipals()
        {
            return await _context.StorePrincipals.ToListAsync();
        }

        // GET: api/StorePrincipals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorePrincipal>> GetStorePrincipal(int id)
        {
            var storePrincipal = await _context.StorePrincipals.FindAsync(id);

            if (storePrincipal == null)
            {
                return NotFound();
            }

            return storePrincipal;
        }

        // PUT: api/StorePrincipals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorePrincipal(int id, StorePrincipal storePrincipal)
        {
            if (id != storePrincipal.Id)
            {
                return BadRequest();
            }

            _context.Entry(storePrincipal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorePrincipalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StorePrincipals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StorePrincipal>> PostStorePrincipal(StorePrincipal storePrincipal)
        {
            _context.StorePrincipals.Add(storePrincipal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStorePrincipal", new { id = storePrincipal.Id }, storePrincipal);
        }

        // DELETE: api/StorePrincipals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorePrincipal(int id)
        {
            var storePrincipal = await _context.StorePrincipals.FindAsync(id);
            if (storePrincipal == null)
            {
                return NotFound();
            }

            _context.StorePrincipals.Remove(storePrincipal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StorePrincipalExists(int id)
        {
            return _context.StorePrincipals.Any(e => e.Id == id);
        }
    }
}
