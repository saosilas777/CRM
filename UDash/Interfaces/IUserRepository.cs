﻿using CRM.Models;
using CRM.Models.ViewModels;

namespace CRM.Interfaces
{
	public interface IUserRepository
	{
		bool Create(LoginModel login);

		UserModel BuscarPorLogin(LoginViewModel login);
		UserModel BuscarPorEmaileLogin(string login, string email);
		List<UserModel> BuscarTodos();
		UserModel BuscarPorId(Guid id);
		UserModel Editar(UserModel contato);
		bool Deletar(Guid id);



	}
}
