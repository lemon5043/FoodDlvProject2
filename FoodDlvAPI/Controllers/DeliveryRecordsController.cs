using FoodDlvAPI.EFModels;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.Services;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using FoodDlvAPI.Models;

namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryRecordsController : Controller
    {
        private readonly DeliveryRecordService deliveryRecordService;

        public DeliveryRecordsController()
        {
            var db = new AppDbContext();
            IDeliveryRecordsRepository repository = new DeliveryRecordsRepository(db);
            this.deliveryRecordService = new DeliveryRecordService(repository);
        }
        [HttpGet("{id}")]
        // GET: DeliveryRecords/MonthlyDetails/5
        public async Task<IEnumerable<DeliveryMonthlyDetailRecordVM>> MonthlyDetails(int? id)
        {

            var data = await deliveryRecordService.GetMonthlyRecordAsync(id);
            //ViewBag.DriverId = id;
            ViewBag.DriverName = data.Select(x => x.DriverName).FirstOrDefault();

            return data.Select(x => x.ToDeliveryMonthlyDetailRecordVM());

        }

        [HttpGet("{year}/{month}/{id}")]
        // GET: DeliveryRecords/IndividualMonthlyDetails/5
        public async Task<IEnumerable<DeliveryIndividualDetailsRecordVM>> IndividualMonthlyDetails(int? year, int? month, int? id)
        {
            var data = await deliveryRecordService.GetIndividualMonthlyRecordAsync(year, month, id);

            //ViewBag.Year = year;
            //ViewBag.Month = month;
            //ViewBag.DriverId = id;
            //ViewBag.DriverName = data.Select(x => x.DriverName).FirstOrDefault();

            return data.Select(x => x.ToDeliveryIndividualDetailsRecordVM());

        }
    }
}
