using CRM.Data;
using CRM.Interfaces;
using CRM.Models;
using Newtonsoft.Json;

namespace CRM.Services
{
	public class AnalyticsServices
	{
		private readonly Context _context;
		private readonly Interfaces.IUserSession _session;

		public AnalyticsServices(Context context, Interfaces.IUserSession section)
		{
			_context = context;
			_session = section;
		}
		public AnalyticsModel AnalyticsBuilder()
		{
			var user = _session.GetUserSection();


			if (user != null)
			{
				DateTime date = DateTime.Now;

				string initialDate = "";
				string finalDate = "";
				if (date.Day > 20 && date.Day <= 31)
				{
					initialDate = $"{date.Year}/{date.Month}/21";
					finalDate = $"{date.Year}/{date.Month + 1}/20";
				}
				else if (date.Month == 1)
				{
					initialDate = $"{date.Year - 1}/12/21";
					finalDate = $"{date.Year}/{date.Month}/20";
				}
				else
				{
					initialDate = $"{date.Year}/{date.Month - 1}/21";
					finalDate = $"{date.Year}/{date.Month}/20";
				}



				List<_CustomerModel> customer = _context.Customers.Where(x => x.UserId == user.Id).ToList();

				double total = 0;
				int active = 0;
				int inactive = 0;


				foreach (var item in customer)
				{
					if (item.LastPurchaseDate >= DateTime.Parse(initialDate) && item.LastPurchaseDate <= DateTime.Parse(finalDate))
					{
						var items = 0;
						total += item.LastPurchaseValue.Value;
					}

					if (item.Status == true)
					{
						active++;
					}
					else
					{
						inactive++;
					}

				}

				double commission = 0;
				switch (total)
				{
					case <= 39999:
						commission = total * 0.010;
						break;
					case <= 49999:
						commission = total * 0.011;
						break;
					case <= 59999:
						commission = total * 0.012;
						break;
					case <= 69999:
						commission = total * 0.013;
						break;
					case <= 79999:
						commission = total * 0.014;
						break;
					case <= 89999:
						commission = total * 0.015;
						break;
					case <= 99999:
						commission = total * 0.016;
						break;
					case <= 109999:
						commission = total * 0.017;
						break;
					case <= 119999:
						commission = total * 0.018;
						break;
					case <= 129999:
						commission = total * 0.019;
						break;
					case > 129999:
						commission = total * 0.020;
						break;
				}

				double pwr = commission * 0.15;
				AnalyticsModel analytics = new();

				analytics.TotalSalesMonth = total;
				analytics.TotalCustomers = customer.Count();
				analytics.ActiveCustomers = active;
				analytics.InactiveCustomers = inactive;

				analytics.BaseSalary = 2052.63;
				analytics.Commission = commission;
				analytics.PaidWeeklyRest = pwr;
				analytics.TotalPayment = 2052.63 + commission + pwr;


				//TotalAnnualSales


				TotalAnnualSales totalAnnualSales = new TotalAnnualSales();
				
				for(int i = 0;i < customer.Count();i++)
				{
					if (customer[i].LastPurchaseDate >= DateTime.Parse("2023-12-21") && customer[i].LastPurchaseDate <= DateTime.Parse("2024-01-20"))
					{
						totalAnnualSales.January += customer[i].LastPurchaseValue.Value;
					}
					if (customer[i].LastPurchaseDate >= DateTime.Parse("2024-01-21") && customer[i].LastPurchaseDate <= DateTime.Parse("2024-02-20"))
					{
						totalAnnualSales.February += customer[i].LastPurchaseValue.Value;
					}

				}
				
				analytics.TotalAnnualSales = totalAnnualSales;


				return analytics;
			}
			return new AnalyticsModel();


		}


	}
}
