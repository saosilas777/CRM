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
		private readonly AnalyticsServices _analyticsServices;
		private readonly ISection _section;
		
		public AnalyticsController(AnalyticsServices analyticsServices, ISection section)
		{
			_analyticsServices = analyticsServices;
			_section = section;
		}

		public IActionResult Index()
		{
			try
			{
				var token = _section.GetUserSection();
				if (TokenService.TokenIsValid(token))
				{
					token = _section.GetUserSection();
					TokenService.TokenIsValid(token);
					var analytics = _analyticsServices.AnalyticsBuilder();
					return View(analytics);
				}
				return RedirectToAction("Login", "Login");
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
			
		}

	}
}