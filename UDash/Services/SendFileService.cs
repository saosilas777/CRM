using OfficeOpenXml;
using System.ComponentModel;
using CRM.Interfaces;
using CRM.Models;
using LicenseContext = System.ComponentModel.LicenseContext;
using System.Collections.Generic;
using CRM.Models.ViewModels;

namespace CRM.Services
{
	public class SendFileService
	{
		private readonly ISection _section;

		public SendFileService(ISection section)
		{
			_section = section;
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
					_contactRecords.RegistrationDate = DateTime.Parse(worksheet.Cells[row, 12].Value.ToString());
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
						ContactRecords = contatos,
						NextContactDate = DateTime.Parse(worksheet.Cells[row, 13].Value.ToString()),
						UserId = user.Id,
						
						
					};

					response.Add(customer);
				}
			}
			return response;

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
