using Microsoft.AspNetCore.Mvc;
using System.Text;
using CRM.Interfaces;
using CRM.Models;
using CRM.Repository;
using CRM.Services;
using System.Collections.Generic;

namespace CRM.Controllers
{
	public class SendFileController : Controller
	{
		private readonly SendFileService _sendFileServices;
		private readonly SendFileImageRepository _sendFileImageRepository;


		private readonly ISection _section;
		private readonly ICustomerRepository _customerRepository;

		public SendFileController(ISection section, SendFileService sendFileServices, ICustomerRepository customerRepository,SendFileImageRepository sendFileImage)
		{
			
			_section = section;
			_sendFileServices = sendFileServices;
			_customerRepository = customerRepository;
			_sendFileImageRepository = sendFileImage;
		}

		[HttpPost]
		public IActionResult SendFile(IFormFile uploadFile)
		{
			try
			{
				
				if(uploadFile != null )
				{
					string extension = Path.GetExtension(uploadFile.FileName);
					if (extension == ".xlsx")
					{
						List<_CustomerModel> customers = _sendFileServices.ReadXls(uploadFile);
						List<_CustomerModel> newList = VerifyDuplicate(customers);
						_customerRepository.AtualizarTodos(newList);
						return RedirectToAction("Index", "Customer");
					}

					TempData["ErrorMessage"] = "O formato do arquivo precisa ser \".xlsx\" !";
					return RedirectToAction("Index", "Customer");
				}
				TempData["ErrorMessage"] = "Nenhum arquivo foi selecionado, tente novamente!";
				return RedirectToAction("Index", "Customer");
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		public IActionResult sendFileImage()
		{
			return View();
		}

		[HttpPost]
		public IActionResult sendFileImage(string url)
		{
			try
			{
				if(url == null || url == "")
				{
					TempData["ErrorMessage"] = "Nenhum arquivo foi selecionado ou não é suportado, tente novamente!";
					return RedirectToAction("sendFileImage", "SendFile");
				}

				var user = TokenService.GetDataInToken(_section.GetUserSection());
				SendFileImageModel image = new SendFileImageModel();
				image.Url = url;
				image.UserId = user.Id;

				_sendFileImageRepository.SendFileImage(image);
				return View(image);

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		public IActionResult EnviarArquivo()
		{
			return RedirectToAction("Index", "Home");
		}

		public List<_CustomerModel> VerifyDuplicate(List<_CustomerModel> customer)
		{
			var token = _section.GetUserSection();
			var user = TokenService.GetDataInToken(token);
			List<_CustomerModel> customerDb = _customerRepository.BuscarTodos(user.Id);

			var month = DateTime.Now.Month;

			
			for (int i = 0; i < customerDb.Count; i++)
			{
				for (int j = 0; j < customer.Count; j++)
				{
					if (customer[j].Cnpj == customerDb[i].Cnpj)
					{
						customerDb[i].LastPurchaseValue = customer[j].LastPurchaseValue;
						customerDb[i].LastPurchaseDate = customer[j].LastPurchaseDate;
						if(customer[j].LastPurchaseDate.Month == month)
						{
							customerDb[i].Status = true;
						}
											
					}
					
				}

			}

			if(customerDb.Count() < customer.Count())
			{
				List<_CustomerModel> newCustomers = new();

				int TotalNewCustomers = customer.Count() - customerDb.Count();
				int total = customerDb.Count() + TotalNewCustomers;

				for (int i = 1; i <= TotalNewCustomers; i++)
				{

					newCustomers.Add(customer[total - i]);
				}
				_customerRepository.AdicionarTodos(newCustomers);
			}

			return customerDb;
		}
		public MemoryStream ReadStrem(IFormFile file)
		{
			using var stream = new MemoryStream();

			file?.CopyTo(stream);
			var byteArray = stream.ToArray();
			return new MemoryStream(byteArray);
		}
	}
}
