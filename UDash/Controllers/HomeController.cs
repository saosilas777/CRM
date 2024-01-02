using Microsoft.AspNetCore.Mvc;
using CRM.Interfaces;
using CRM.Models;
using CRM.Services;

namespace CRM.Controllers
{
	public class HomeController : Controller
	{
		#region Dependencies
		private readonly ISection _section;
		public HomeController(ISection section)
		{
			_section = section;

		}

		#endregion

		public IActionResult Index()
		{
				return View();			
			
		}
	}
}
