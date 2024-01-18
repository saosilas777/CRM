using CRM.Data;
using CRM.Models;

namespace CRM.Services
{
	public  class AnalyticsServices
	{
		private readonly Context _context;

		public AnalyticsServices(Context context)
		{
			_context = context;
		}
		public AnalyticsModel AnalyticsBuilder()
		{
			DateTime date = DateTime.Now;
			
			var month = date.Month;

			List<_CustomerModel> customer = _context.Customers.ToList();

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
