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
        public async Task<IActionResult> MonthlyDetails(int? id)
        {
			var data = await paysService.GetMonthlyDetailsAsync(id);
			var VM = data.Select(x => x.ToPaysMonthlyDetailsVM());

			ViewBag.DriverId = id;
			ViewBag.DriverName = VM.Select(x=>x.DriversName).FirstOrDefault();

			return View(VM);
        }

        // GET: Pays/IndividualMonthly/5
        public async Task<IActionResult> IndividualMonthlyDetails(int? year, int? month, int? id)
		{
			var data = await paysService.GetIndividualMonthlyDetailsAsync(year, month, id);				
			return View(data.ToPaysIndividualMonthlyDetailsVM());
		}

	}
}
