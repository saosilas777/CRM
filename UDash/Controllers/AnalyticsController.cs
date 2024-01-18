using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repository;
using CRM.Services;

namespace CRM.Controllers
{
	public class AnalyticsController : Controller
	{
		private readonly AnalyticsRepository _analyticsRepository;
		private readonly AnalyticsServices _analyticsServices;

		public AnalyticsController(AnalyticsRepository analyticsRepository,
									AnalyticsServices analyticsServices)
		{
			_analyticsRepository = analyticsRepository;
			_analyticsServices = analyticsServices;
		}

		public IActionResult Index()
		{
			var analytics = _analyticsServices.AnalyticsBuilder();
			return View(analytics);
		}

		/*[HttpPost]
		public IActionResult ReceberDados(AnalyticsViewModel analytics)
		{
			_analyticsRepository.InsertAnalytics(analytics);
			return RedirectToAction("Index", "Analytics");
		}*/
	}
}