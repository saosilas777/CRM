﻿using Microsoft.AspNetCore.Mvc;
using System.Text;
using CRM.Interfaces;
using CRM.Models;
using CRM.Repository;
using CRM.Services;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CRM.Controllers
{
	public class SendFileController : Controller
	{
		private readonly SendFileService _sendFileService;
		private readonly SendFileImageRepository _sendFileImageRepository;


		private readonly Interfaces.IUserSession _session;
		private readonly ICustomerRepository _customerRepository;


		public SendFileController(Interfaces.IUserSession section, SendFileService sendFileService, 
								  ICustomerRepository customerRepository,
								  SendFileImageRepository sendFileImage)
		{
			_session = section;
			_sendFileService = sendFileService;
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
						List<_CustomerModel> customers = _sendFileService.ReadXls(uploadFile);
						
						List<_CustomerModel> newList = _sendFileService.VerifyDuplicate(customers);

						
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

				TempData["ErrorMessage"] = "Não foi possível ler o arquivo inserido, se o erro persistir verifique se não há falhas no arquivo.";
				return RedirectToAction("Index", "Customer");
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

				var user = _session.GetUserSection();
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

		
		
	}
}
