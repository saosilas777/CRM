/*using Microsoft.AspNetCore.Mvc;
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
		private readonly ISection _section;
		private readonly AnalyticsRepository _analyticsRepository;

		public AnalyticsController(ISection section, AnalyticsRepository analyticsRepository)
		{
			_section = section;			
			_analyticsRepository = analyticsRepository;
		}

		public IActionResult Index()
		{
			var analytics = _analyticsRepository.BuscarTodos();
			return View(analytics);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ReceberDatos(AnalyticsViewModel analytics)
		{
			_analyticsRepository.InsertAnalytics(analytics);
			return RedirectToAction("Index", "Analytics");
		}
	}
}*/