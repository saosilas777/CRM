using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repository;
using CRM.Services;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;

namespace CRM.Controllers
{
	public class CustomerController : Controller
	{
		#region Dependencies
		private readonly ICustomerRepository _customer;
		private readonly ISection _section;
		public CustomerController(ICustomerRepository customer,
									ISection section
									)
		{
			_customer = customer; _section = section;

		}
		#endregion


		public IActionResult Index()
		{
			try
			{
				var token = _section.GetUserSection();
				var validationToken = TokenService.TokenIsValid(token);
				if (validationToken == true)
				{
					var user = TokenService.GetDataInToken(token);

					List<_CustomerModel> customers = _customer.BuscarTodos(user.Id);



					return View(customers);
				}
				return RedirectToAction("Login", "Login");

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
			
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
				var token = _section.GetUserSection();
				var user = TokenService.GetDataInToken(token);
				if (initialDate != null && finalDate != null)
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
			return RedirectToAction("Editar", "Customer",new { customer.Id });
		}

		[HttpPost]
		public IActionResult Delete(_CustomerModel customer)
		{
			_customer.Deletar(customer);
			return RedirectToAction("Index", "Customer");
		}


	}
}
