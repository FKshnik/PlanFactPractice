using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlanFactPractice.Models;
using PlanFactPractice.Utilities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PlanFactPractice.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private static DateRangeModel _dateRange { get; set; } = new DateRangeModel();
		public static string prop = "Hello World";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View(_dateRange);
		}

		public void ChangeDate(int id, string date)
		{
			DateTime changedDate = DateTime.ParseExact(date, "yyyy-MM", System.Globalization.CultureInfo.InvariantCulture);
			if (id == 0)
				_dateRange.StartDate = changedDate;
			else
				_dateRange.EndDate = changedDate.AddMonths(1).AddDays(-1);
		}

		[HttpGet]
		public JsonResult BuildTable()
		{
			return Json(new { success = true, content = GraphBuilderHelper.BuildValuesTable(_dateRange) });
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
