using FoodDlvAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Net;
using System.Net;
using System.Xml.Linq;

namespace FoodDlvAPI.Controllers
{
    public class MemberAddressController : Controller
    {
        private readonly AppDbContext _context;

        public MemberAddressController(AppDbContext context)
        {
            _context = context;
        }


        // GET: MemberAddressController
        [HttpGet("index")]
        public ActionResult Index(int memberId)
        {
            var data = _context.AccountAddresses.Where(aa => aa.MemberId == memberId).ToList();
            return Json(data);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(int memberId, string address)
        {
            try
            {
                var validationAddress = await GetOriginsLongitudeNLatitude(address);
                var data = new AccountAddress
                {
                    MemberId = memberId,
                    Address = address,

                };

                _context.AccountAddresses.Add(data);
                _context.SaveChanges();                
                return new EmptyResult();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        [HttpPost("Edit")]
        public async Task<ActionResult> Edit(int id, int memberId, string address)
        {
            try
            {
                var validationAddress = await GetOriginsLongitudeNLatitude(address);
                var data = _context.AccountAddresses.First(aa => aa.Id == id && aa.MemberId == memberId);
                data.Address = address;

                _context.AccountAddresses.Update(data);
                _context.SaveChanges();
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = _context.AccountAddresses.First(aa => aa.Id == id);                
                _context.AccountAddresses.Remove(data);
                _context.SaveChanges();
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetOriginsLongitudeNLatitude")]
        public async Task<List<double>> GetOriginsLongitudeNLatitude(string origin)
        {
            var apiKey = await _context.Apis.Where(x => x.Id == 1).Select(x => x.Apikey).FirstOrDefaultAsync();
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={origin}&key={apiKey}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);

            var OriginsLongitude = Convert.ToDouble(result.results[0].geometry.location.lng);
            var OriginsLatitude = Convert.ToDouble(result.results[0].geometry.location.lat);
            var OriginsLongitudeNLatitude = new List<double>() { OriginsLongitude, OriginsLatitude };

            return OriginsLongitudeNLatitude;
        }
    }
}
