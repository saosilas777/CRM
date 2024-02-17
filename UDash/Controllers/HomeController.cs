using Microsoft.AspNetCore.Mvc;
using CRM.Interfaces;
using CRM.Models;
using CRM.Services;
using System.Reflection.PortableExecutable;

namespace CRM.Controllers
{
	public class HomeController : Controller
	{
		#region Dependencies
		private readonly Interfaces.IUserSession _session;
		public HomeController(Interfaces.IUserSession section)
		{
			_session = section;

		}

		#endregion

		public IActionResult Index(UserModel user)
		{

			return View(user);
			
		}
	}
}
