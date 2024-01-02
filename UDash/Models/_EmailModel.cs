namespace CRM.Models
{
	public class _EmailModel
	{
		public Guid Id { get; set; }
		public string? Email { get; set; } = string.Empty;
		public DateTime? RegistrationDate { get; set; }
		public _CustomerModel Customer { get; set; }

	}
}
