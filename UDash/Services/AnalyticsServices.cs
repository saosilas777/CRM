using CRM.Data;
using CRM.Interfaces;
using CRM.Models;

namespace CRM.Services
{
	public  class AnalyticsServices
	{
		private readonly Context _context;
		private readonly ISection _section;

		public AnalyticsServices(Context context,ISection section)
		{
			_context = context;
			_section = section;
		}
		public AnalyticsModel AnalyticsBuilder()
		{
			var token = _section.GetUserSection();
			var user = TokenService.GetDataInToken(token);


			DateTime date = DateTime.Now;
			
			var month = date.Month;

			List<_CustomerModel> customer = _context.Customers.Where(x => x.UserId == user.Id).ToList();

			double? count = 0;
			int active = 0;
			int inactive = 0;
			foreach (var item in customer)
			{
				if(item.LastPurchaseDate.Month == month)
				{
					count += item.LastPurchaseValue;
				}
				if(item.Status == true)
				{
					active++;
				}
				else
				{
					inactive++;
				}

			}

			AnalyticsModel analytics = new();

			analytics.TotalSalesMonth = count;
			analytics.TotalCustomers = customer.Count();
			analytics.ActiveCustomers = active;
			analytics.InactiveCustomers = inactive;

			return analytics;

		}


	}
}
