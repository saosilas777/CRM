﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repository;
using CRM.Services;
using Newtonsoft.Json;

namespace CRM.Controllers
{
	public class AnalyticsController : Controller
	{
		private readonly AnalyticsServices _analyticsServices;
		private readonly Interfaces.IUserSession _session;
		private readonly ICustomerRepository _customer;

		public AnalyticsController(AnalyticsServices analyticsServices, Interfaces.IUserSession section, ICustomerRepository customer)
		{
			_analyticsServices = analyticsServices;
			_session = section;
			_customer = customer;
		}

		public IActionResult Index()
		{
			try
			{
				var user = _session.GetUserSection();
				
				if (user != null)
				{
					var analytics = _analyticsServices.AnalyticsBuilder();
					return View(analytics);
				}
				return RedirectToAction("NonUserPage", "User");
				/*else
				{
					_session.UserSectionRemove();
					return RedirectToAction("Login", "Login");
				}*/

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}

		}

	}
}