namespace CRM.Models
{
	public class AnalyticsModel
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime Registration { get; set; }
		public int TotalCustomers { get; set; }
		public int ActiveCustomers { get; set; }
		public int InactiveCustomers { get; set; }
		public double? TotalSalesMonth { get; set; }

	}
}
