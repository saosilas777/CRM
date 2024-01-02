using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
	public class _CustomerModel
	{
		[Key]
		public Guid Id { get; set; }
		public string Codigo { get; set; } = string.Empty;
		public string Cnpj { get; set; } = string.Empty;
		public string RazaoSocial { get; set; } = string.Empty;
		public bool Status { get; set; }
		public string Cidade { get; set; } = string.Empty;
		public string Uf { get; set; } = string.Empty;
		public List<_EmailModel>? Emails { get; set; }
		public List<_PhoneModel>? Phones { get; set; }
		public string Contact { get; set; }
		public DateTime LastPurchaseDate { get; set; }
		public double? LastPurchaseValue { get; set; }
		public List<_ContactRecords>? ContactRecords { get; set; }
		public DateTime NextContactDate { get; set; }
		public Guid UserId { get; set; }

	}
}
