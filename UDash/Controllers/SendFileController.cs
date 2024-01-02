using Microsoft.AspNetCore.Mvc;
using System.Text;
using CRM.Interfaces;
using CRM.Models;
using CRM.Repository;
using CRM.Services;

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
			List<_CustomerModel> contatos = _sendFileServices.ReadXls(uploadFile);
			/*List<CustomerModel> newList = VerifyDuplicate(contatos);*/
			_customerRepository.AdicionarTodos(contatos);
			return RedirectToAction("Index", "Customer");
		}

		public IActionResult sendFileImage()
		{
			Guid id = Guid.Parse("C4136153-0D9D-4D31-A54D-E413AAE51D1D");
			var img = _sendFileImageRepository.GetById(id);
			return View(img);
		}

		[HttpPost]
		public IActionResult sendFileImage(string url)
		{
			var user = TokenService.GetDataInToken(_section.GetUserSection());
			SendFileImageModel image = new SendFileImageModel
			{
				Id = Guid.NewGuid(),
				Url = url,
				UserId = user.Id

			};
						
			_sendFileImageRepository.SendFileImage(image);
			return View(image);
		}

		public IActionResult EnviarArquivo()
		{
			return RedirectToAction("Index", "Home");
		}

		/*public List<_CustomerModel> VerifyDuplicate(List<_CustomerModel> customer)
		{
			var token = _section.GetUserSection();
			var user = TokenService.GetDataInToken(token);
			List<_CustomerModel> customerDb = _customerRepository.BuscarTodos(user.Id);

			for (int i = 0; i < customerDb.Count; i++)
			{
				for (int j = 0; j < customer.Count; j++)
				{
					if (customer[j].IdSense == customerDb[i].IdSense)
					{
						customer.Remove(customer[j]);
					}
				}

			}
			return customer;
		}*/
		public MemoryStream ReadStrem(IFormFile file)
		{
			using var stream = new MemoryStream();

			file?.CopyTo(stream);
			var byteArray = stream.ToArray();
			return new MemoryStream(byteArray);
		}
	}
}
