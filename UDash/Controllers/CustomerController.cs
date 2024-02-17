using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repository;
using CRM.Services;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CRM.Controllers
{
	public class CustomerController : Controller
	{
		#region Dependencies
		private readonly ICustomerRepository _customer;
		private readonly IUserSession _session;
		public CustomerController(ICustomerRepository customer,
                                   IUserSession session
									)
		{
			_customer = customer; 
			_session = session;

		}
		#endregion
		
		public IActionResult Index()
		{
			var user = _session.GetUserSection();
			List<_CustomerModel> customers = _customer.BuscarTodos(user.Id);
			return View(customers);
		}

		public IActionResult Editar(Guid id)
		{
			_CustomerEditViewModel customerDb = _customer.BuscarPorId(id);


			return View(customerDb);
		}

		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public IActionResult FindCustomerByDate(string initialDate, string finalDate)
		{
			try
			{
				var user = _session.GetUserSection();
				if (initialDate != null && finalDate != null && user != null)
				{

					var customers = _customer.BuscarTodos(user.Id).Where(x => x.NextContactDate >= DateTime.Parse(initialDate) && x.NextContactDate <= DateTime.Parse(finalDate)).ToList();
					return View("Index", customers);
				}
				else
				{
					return RedirectToAction("Index", "Customer");
				}

			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpPost]
		public IActionResult Create(_CustomerCreateViewModel customer)
		{
			_customer.Create(customer);
			return View();
		}

		[HttpPost]
		public IActionResult Edit(_CustomerEditViewModel customer)
		{
			_customer.Atualizar(customer);
			return RedirectToAction("Index", "Customer");
		}
		[HttpPost]
		public IActionResult RegistrationContact(_CustomerEditViewModel customer)
		{
			_customer.RegistrationContact(customer.ContactRecordsAnotation[0], customer.NextContactDate, customer.Id);
			return RedirectToAction("Editar", "Customer", new { customer.Id });
		}

		[HttpPost]
		public IActionResult Delete(_CustomerModel customer)
		{
			_customer.Deletar(customer);
			return RedirectToAction("Index", "Customer");
		}


	}
}
