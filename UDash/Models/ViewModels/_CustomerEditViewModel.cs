﻿using System.ComponentModel.DataAnnotations;

namespace CRM.Models.ViewModels
{
	public class _CustomerEditViewModel
	{
		[Key]
		public Guid Id { get; set; }
		public string Codigo { get; set; } = string.Empty;
		public string Cnpj { get; set; } = string.Empty;
		public string RazaoSocial { get; set; } = string.Empty;
		public bool Status { get; set; }
		public string Cidade { get; set; } = string.Empty;
		public string Uf { get; set; } = string.Empty;
		public string[] Emails { get; set; } = new string[3];
		public string[] Phones { get; set; } = new string[3];
		public string Contact { get; set; }
		public DateTime LastPurchaseDate { get; set; }
		public double? LastPurchaseValue { get; set; }
		public string[] ContactRecordsDate { get; set; } = new string[3];
		public string[] ContactRecordsAnotation { get; set; } = new string[3];
		public DateTime NextContactDate { get; set; }
		public Guid UserId { get; set; }
	}
}
