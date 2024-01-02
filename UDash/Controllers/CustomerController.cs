using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repository;
using CRM.Services;

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
			var token = _section.GetUserSection();
			var user = TokenService.GetDataInToken(token);
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
		public IActionResult Create(_CustomerCreateViewModel customer)
		{

			return View();
		}

		[HttpPost]
		public IActionResult Edit(_CustomerEditViewModel customer)
		{
			
			return RedirectToAction("Index", "Customer");
		}

		[HttpPost]
		public IActionResult Delete(_CustomerModel customer)
		{
			_customer.Deletar(customer);
			return RedirectToAction("Index", "Customer");
		}


	}
}
