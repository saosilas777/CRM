using CRM.Models.ViewModels;
using CRM.Models;

namespace CRM.Interfaces
{
	public interface ICustomerRepository
	{
		bool Create(_CustomerCreateViewModel customer);
		_CustomerEditViewModel BuscarPorId(Guid id);
		List<_CustomerModel> BuscarTodos(Guid id);
		/*bool Editar(_CustomerModel customer);*/
		bool Deletar(_CustomerModel customer);
		List<_CustomerModel> AdicionarTodos(List<_CustomerModel> customers);
		_CustomerModel Atualizar(_CustomerEditViewModel customers);
		List<_CustomerModel> AtualizarTodos(List<_CustomerModel> customers);

		bool RegistrationContact(string anotation,string date, Guid id);
		void TokenValidationRegister(testeTokenValid token);



	}
}
