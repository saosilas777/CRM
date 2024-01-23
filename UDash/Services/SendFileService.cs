using OfficeOpenXml;
using System.ComponentModel;
using CRM.Interfaces;
using CRM.Models;
using LicenseContext = System.ComponentModel.LicenseContext;
using System.Collections.Generic;
using CRM.Models.ViewModels;
using CRM.Repository;

namespace CRM.Services
{
	public class SendFileService
	{
		private readonly ISection _section;
		private readonly ICustomerRepository _customerRepository;

		public SendFileService(ISection section, ICustomerRepository customerRepository)
		{
			_section = section;
			_customerRepository = customerRepository;
		}

		public List<_CustomerModel> ReadXls(IFormFile uploadFile)
		{
			var streamFile = ReadStrem(uploadFile);

			string token = _section.GetUserSection();
			UserModel user = TokenService.GetDataInToken(token);
			List<_CustomerModel> response = new();
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;


			using (ExcelPackage package = new((Stream)streamFile))
			{

				ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
				int colCount = worksheet.Dimension.End.Column;
				int rowCount = worksheet.Dimension.End.Row;

				if (colCount <= 4)
				{
					for (int row = 2; row <= rowCount; row++)
					{
						_CustomerModel customer = new _CustomerModel()
						{

							Codigo = worksheet.Cells[row, 1].Value.ToString(),
							RazaoSocial = worksheet.Cells[row, 2].Value.ToString(),
							LastPurchaseDate = DateTime.Parse(worksheet.Cells[row, 3].Value.ToString()),
							LastPurchaseValue = double.Parse(worksheet.Cells[row,4].Value.ToString()),
							
							UserId = user.Id,


						};
						response.Add(customer);
					};
					return response;
				}
				else
				{
					for (int row = 2; row <= rowCount; row++)
					{
						bool status;
						if (worksheet.Cells[row, 4].Value.ToString() == "Ativo" || worksheet.Cells[row, 4].Value.ToString() == "ATIVO")
						{
							status = true;
						}
						else
						{
							status = false;
						}



						_EmailModel _emailModel = new();
						_emailModel.Id = Guid.NewGuid();
						_emailModel.Email = worksheet.Cells[row, 5].Value.ToString();
						_emailModel.RegistrationDate = DateTime.Now;
						List<_EmailModel> emails = new List<_EmailModel>();
						emails.Add(_emailModel);

						_PhoneModel _phoneModel = new();
						_phoneModel.Id = Guid.NewGuid();
						_phoneModel.Phone = worksheet.Cells[row, 6].Value.ToString();
						_phoneModel.RegistrationDate = DateTime.Now;
						List<_PhoneModel> phones = new();
						phones.Add(_phoneModel);

						_ContactRecords _contactRecords = new();
						_contactRecords.Id = Guid.NewGuid();
						/*_contactRecords.RegistrationDate = DateTime.Parse(worksheet.Cells[row, 12].Value.ToString());*/
						_contactRecords.RegistrationDate = DateTime.Now;

						List<_ContactRecords> contatos = new();
						contatos.Add(_contactRecords);



						_CustomerModel customer = new _CustomerModel()
						{

							Codigo = worksheet.Cells[row, 1].Value.ToString(),
							Cnpj = worksheet.Cells[row, 2].Value.ToString(),
							RazaoSocial = worksheet.Cells[row, 3].Value.ToString(),
							Status = status,
							Emails = emails,
							Phones = phones,
							Contact = worksheet.Cells[row, 7].Value.ToString(),
							Cidade = worksheet.Cells[row, 8].Value.ToString(),
							Uf = worksheet.Cells[row, 9].Value.ToString(),
							LastPurchaseDate = DateTime.Parse(worksheet.Cells[row, 10].Value.ToString()),
							LastPurchaseValue = double.Parse(worksheet.Cells[row, 11].Value.ToString()),
							NextContactDate = DateTime.Parse(worksheet.Cells[row, 13].Value.ToString()),
							UserId = user.Id,


						};

						response.Add(customer);
					}
					VerifyDuplicatedCustomers(response);
				}
				return response;
			}
		}
		public List<_CustomerModel> VerifyDuplicate(List<_CustomerModel> customer)
		{
			var token = _section.GetUserSection();
			var user = TokenService.GetDataInToken(token);
			List<_CustomerModel> customerDb = _customerRepository.BuscarTodos(user.Id);

			var month = DateTime.Now.Month;


			for (int i = 0; i < customerDb.Count(); i++)
			{
				for (int j = 0; j < customer.Count(); j++)
				{
					if (customer[j].Codigo.ToString() == customerDb[i].Codigo.ToString())
					{
						customerDb[i].LastPurchaseValue = customer[j].LastPurchaseValue;
						customerDb[i].LastPurchaseDate = customer[j].LastPurchaseDate;
						if (customer[j].LastPurchaseDate.Month == month)
						{
							customerDb[i].Status = true;
						}
					}
				}

			}
			_customerRepository.AtualizarTodos(customerDb);

			return customerDb;


		}

		public void VerifyDuplicatedCustomers(List<_CustomerModel> customer)
		{
			var token = _section.GetUserSection();
			var user = TokenService.GetDataInToken(token);
			List<_CustomerModel> customerDb = _customerRepository.BuscarTodos(user.Id);

			if (customerDb.Count() < customer.Count())
			{
				for (int i = 0; i < customer.Count(); i++)
				{
					for (int j = 0; j < customerDb.Count(); j++)
					{
						if (customerDb[j].Codigo == customer[i].Codigo)
						{

							customer.Remove(customer[i]);
						}
					}

				}
				_customerRepository.AdicionarTodos(customer);
			}
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
