namespace CRM.Models
{
	public class _ContactRecords
	{
		public Guid Id { get; set; }
		public string Anotation { get; set; } = string.Empty;
		public DateTime RegistrationDate { get; set; }
		public _CustomerModel Customer { get; set; }
	}
}
