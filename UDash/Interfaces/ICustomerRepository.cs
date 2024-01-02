using CRM.Models.ViewModels;
using CRM.Models;

namespace CRM.Interfaces
{
	public interface ICustomerRepository
	{
		bool Create(_CustomerModel customer);
		_CustomerEditViewModel BuscarPorId(Guid id);
		List<_CustomerModel> BuscarTodos(Guid id);
		/*bool Editar(_CustomerModel customer);*/
		bool Deletar(_CustomerModel customer);
		List<_CustomerModel> AdicionarTodos(List<_CustomerModel> customers);



	}
}
