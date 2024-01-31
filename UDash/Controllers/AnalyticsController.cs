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
		private readonly ICustomerRepository _customer;

		public AnalyticsController(AnalyticsServices analyticsServices, ISection section, ICustomerRepository customer)
		{
			_analyticsServices = analyticsServices;
			_section = section;
			_customer = customer;
		}

		public IActionResult Index()
		{
			try
			{
				var token = _section.GetUserSection();
				var tokenRegister = TokenService.TokenIsValid(token);
				_customer.TokenValidationRegister(tokenRegister);
				if (tokenRegister.IsValid == true)
				{
					var analytics = _analyticsServices.AnalyticsBuilder();
					return View(analytics);
				}
				else
				{
					_section.UserSectionRemove();
					return RedirectToAction("Login", "Login");
				}

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}

		}

	}
}