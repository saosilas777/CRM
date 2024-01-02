namespace CRM.Models
{
	public class _PhoneModel
	{
		public Guid Id { get; set; }
		public string? Phone { get; set; } = string.Empty;
		public DateTime? RegistrationDate { get; set; }
		public _CustomerModel Customer { get; set; }
	}
}
