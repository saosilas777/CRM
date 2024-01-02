namespace CRM.Models
{
	public class AnalyticsModel
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime Registration { get; set; }
		public UserModel User { get; set; }
		public double TotalPortfolio { get; set; }
		public int TotalCustomer { get; set; }
		public double AverageTicket { get; set; }
		public int Churns { get; set; }
	}
}
