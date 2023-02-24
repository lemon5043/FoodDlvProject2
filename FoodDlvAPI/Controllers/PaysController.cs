using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models.Services;
using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaysController : Controller
	{
		private readonly PaysService paysService;

		public PaysController()
		{
			var db = new AppDbContext();
			IPaysRepository repository = new PaysRepository(db);
			this.paysService = new PaysService(repository);
		}

        // GET: Pays/IndividualMonthly/5
        public async Task<IEnumerable<PaysMonthlyDetailsVM>> MonthlyDetails(int? id)
        {
			var data = await paysService.GetMonthlyDetailsAsync(id);
			//ViewBag.DriverId = id;
			//ViewBag.DriverName = VM.Select(x=>x.DriversName).FirstOrDefault();

			return data.Select(x => x.ToPaysMonthlyDetailsVM());
        }

        // GET: Pays/IndividualMonthly/5
        public async Task<PaysIndividualMonthlyDetailsVM> IndividualMonthlyDetails(int? year, int? month, int? id)
		{
			var data = await paysService.GetIndividualMonthlyDetailsAsync(year, month, id);				
			return data.ToPaysIndividualMonthlyDetailsVM();
		}

	}
}
