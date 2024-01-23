using Microsoft.EntityFrameworkCore;
using CRM.Data;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Services;

namespace CRM.Repository
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly ISection _section;
		private readonly Context _context;
		public CustomerRepository(Context context,
								ISection section)
		{
			_context = context;
			_section = section;
		}


		public _CustomerEditViewModel BuscarPorId(Guid id)
		{
			var custumerDb = _context.Customers.Include(x => x.ContactRecords).Include(x => x.Emails).Include(x => x.Phones).FirstOrDefault(x => x.Id == id);

			string[] emails = new string[custumerDb.Emails.Count()];
			string[] phones = new string[custumerDb.Phones.Count()];
			string[] anotation = new string[custumerDb.ContactRecords.Count()];
			string[] recordsDate = new string[custumerDb.ContactRecords.Count()];

			for (int i = 0; i < custumerDb.Emails.Count(); i++)
			{
				emails[i] = custumerDb.Emails[i].Email;

			}
			for (int i = 0; i < custumerDb.Phones.Count(); i++)
			{
				phones[i] = custumerDb.Phones[i].Phone;

			}
			for (int i = 0; i < custumerDb.ContactRecords.Count(); i++)
			{
				anotation[i] = custumerDb.ContactRecords[i].Anotation;

			}
			for (int i = 0; i < custumerDb.ContactRecords.Count(); i++)
			{
				recordsDate[i] = custumerDb.ContactRecords[i].RegistrationDate.ToString();

			}


			_CustomerEditViewModel _CustomerEditView = new()
			{
				Id = custumerDb.Id,
				Codigo = custumerDb.Codigo,
				Cnpj = custumerDb.Cnpj,
				RazaoSocial = custumerDb.RazaoSocial,
				Status = custumerDb.Status,
				Cidade = custumerDb.Cidade,
				Uf = custumerDb.Uf,
				Emails = emails,
				Phones = phones,
				Contact = custumerDb.Contact,
				LastPurchaseDate = custumerDb.LastPurchaseDate,
				LastPurchaseValue = custumerDb.LastPurchaseValue,
				ContactRecordsDate = recordsDate,
				ContactRecordsAnotation = anotation,
				NextContactDate = custumerDb.NextContactDate.ToShortDateString()

			};

			return (_CustomerEditView);

		}

		public List<_CustomerModel> BuscarTodos(Guid id)
		{
			return _context.Customers.Include(x => x.Emails).Include(x => x.ContactRecords).Include(x => x.Phones).Where(x => x.UserId == id).ToList();
		}
		public List<_CustomerModel> ListarTodos()
		{
			return _context.Customers.ToList();
		}

		public bool Create(_CustomerModel customer)
		{
			string token = _section.GetUserSection();
			UserModel user = TokenService.GetDataInToken(token);

			customer.UserId = user.Id;
			_context.Add(customer);
			_context.SaveChanges();
			return true;
		}
		public bool CreateAll(List<_CustomerModel> customers)
		{

			_context.AddRange(customers);
			_context.SaveChanges();
			return true;
		}

		public bool Deletar(_CustomerModel customer)
		{
			_context.Remove(customer);
			_context.SaveChanges();
			return true;
		}

		/*public bool Editar(_CustomerModel customer)
		{
			_CustomerModel customerDb = BuscarPorId(customer.Id);

			customerDb.Codigo = customer.Codigo;
			customerDb.RazaoSocial = customer.RazaoSocial;
			customerDb.Contact = customer.Contact;
			customerDb.Status = customer.Status;
			customerDb.LastPurchaseDate = customer.LastPurchaseDate;
			customerDb.LastPurchaseValue = customer.LastPurchaseValue;
			customerDb.ContactRecords = customer.ContactRecords;
			customerDb.NextContactDate = customer.NextContactDate;

			_context.Update(customerDb);
			_context.SaveChanges();
			return true;

		}*/
		public List<_CustomerModel> CreateAt()
		{
			List<_CustomerModel> _customers = new List<_CustomerModel>();

			for (int i = 0; i < 20; i++)
			{
				_CustomerModel customer = new _CustomerModel
				{
					/*IdSense = new Guid().ToString().Substring(0, 6),
					IdStarford = new Guid().ToString().Substring(0, 6),
					Cnpj = "01.001.000/0001-01",
					RazaoSocial = $"Mercado do seu zé {i}",
					Status = true,
					Loja = $"Mercado do seu zé {i}",
					Cliente = $"Mercado do seu zé {i}",
					ProdutoFiscal = "MRR de E-Commerce",
					Fr = "MRR",
					ValorMensal = 599.99*/


				};
				string token = _section.GetUserSection();
				UserModel user = TokenService.GetDataInToken(token);

				customer.UserId = user.Id;
				_customers.Add(customer);
			}



			CreateAll(_customers);
			return _customers;
		}

		public List<_CustomerModel> AtualizarTodos(List<_CustomerModel> customers)
		{
			_context.Customers.UpdateRange(customers);
			_context.SaveChanges();
			return customers;
		}
		
		public _CustomerModel Atualizar(_CustomerEditViewModel _customer)
		{
			var _customerDb = _context.Customers.FirstOrDefault(x => x.Id == _customer.Id);
			var _phonesDb = _context.Phones.Where(x => x.Customer == _customerDb).ToList();
			var _emailsDb = _context.Emails.Where(x => x.Customer == _customerDb).ToList();


			for (int i = 0; i < _customer.Phones.Length; i++)
			{
				_phonesDb[i].Phone = _customer.Phones[i];
			}

			
			for (int i = 0; i < _customer.Emails.Length; i++)
			{
				_emailsDb[i].Email = _customer.Emails[i];
			}

			
			_customerDb.Codigo = _customer.Codigo;
			_customerDb.Cnpj = _customer.Cnpj;
			_customerDb.RazaoSocial = _customer.RazaoSocial;
			_customerDb.Status = _customer.Status;
			_customerDb.Cidade = _customer.Cidade;
			_customerDb.Uf = _customer.Uf;
			_customerDb.Contact = _customer.Contact;
			_customerDb.LastPurchaseDate = _customer.LastPurchaseDate;
			_customerDb.LastPurchaseValue = _customer.LastPurchaseValue;
			_customerDb.NextContactDate = DateTime.Parse(_customer.NextContactDate);


			_context.Customers.Update(_customerDb);
			_context.SaveChanges();
			return _customerDb;
		}
		public List<_CustomerModel> AdicionarTodos(List<_CustomerModel> customers)
		{

			_context.Customers.AddRange(customers);
			_context.SaveChanges();
			return customers;
		}

		public _CustomerModel BuscarCustomerPorId(Guid id)
		{
			var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
			return customer;
		}
		public bool RegistrationContact(string anotation, string date, Guid id)
		{
			var _customer = BuscarCustomerPorId(id);
			_ContactRecords contacts = new();
			contacts.Customer = _customer;
			contacts.Anotation = anotation;
			contacts.RegistrationDate = DateTime.Now;

			_customer.NextContactDate = DateTime.Parse(date);

			_context.Customers.Update(_customer);
			_context.ContactRecords.Add(contacts);
			_context.SaveChanges();

			return true;

		}
		public void NextContactRecord(string date, Guid id)
		{
			var customer = BuscarCustomerPorId(id);
			customer.NextContactDate = DateTime.Parse(date);

			_context.Customers.Update(customer);
			_context.SaveChanges();


		}
	}
}
