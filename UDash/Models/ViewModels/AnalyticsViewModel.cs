namespace CRM.Models.ViewModels
{
	public class AnalyticsViewModel
	{
		public Guid UserId { get; set; }
		public DateTime Registration { get; set; }
		public double TotalPortfolio { get; set; }
		public int TotalCustomer { get; set; }
		public double AverageTicket { get; set; }
		public int Churns { get; set; }
		/*public MeetingsMonths Meetings { get; set; }
		public NoShowsMonth NoShows { get; set; }*/
	}
}
