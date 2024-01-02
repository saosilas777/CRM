/*using Microsoft.EntityFrameworkCore;
using System.Linq;
using CRM.Data;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Services;

namespace CRM.Repository
{
	public class AnalyticsRepository
	{
		private readonly ISection _section;
		private readonly ICustomerRepository _customerRepository;

		private readonly Context _context;
		public AnalyticsRepository(Context context,
								ISection section,
								ICustomerRepository customerRepository
								)
		{
			_context = context;
			_section = section;
			_customerRepository = customerRepository;

		}

		public AnalyticsViewModel BuscarTodos()
		{
			try
			{
				var token = _section.GetUserSection();
				var user = TokenService.GetDataInToken(token);

				List<_CustomerModel> customers = _customerRepository.BuscarTodos(user.Id);

				double totalPortfolio = 0;

				for (int i = 0; i < customers.Count(); i++)
				{
					totalPortfolio += customers[i].ValorMensal;
				}
				AnalyticsModel analytics = _context.Analytics.Where(x => x.UserId == user.Id).OrderByDescending(x => x.Registration).FirstOrDefault();
				MeetingsMonths meetings = _context.Meetings.Where(x => x.UserId == user.Id).OrderByDescending(x => x.Registration).FirstOrDefault();
				NoShowsMonth noShows = _context.NoShows.Where(x => x.UserId == user.Id).OrderByDescending(x => x.Registration).FirstOrDefault();

				if (customers.Any())
				{
					if (analytics == null || meetings == null || noShows == null)
					{
						var _analytics = GetNewEmptyAnalytics();
						double _totalPortfolio = 0;

						for (int i = 0; i < customers.Count(); i++)
						{
							_totalPortfolio += customers[i].ValorMensal;
						}
						_analytics.TotalPortfolio = _totalPortfolio;
						_analytics.AverageTicket = _totalPortfolio / customers.Count();
						_analytics.TotalCustomer = customers.Count();
						InsertAnalytics(_analytics);
						return _analytics;
					}
				}
				else
				{
					var _analytics = GetNewEmptyAnalytics();
					return _analytics;
				}

				AnalyticsViewModel analyticsViewModel = new AnalyticsViewModel
				{
					AverageTicket = totalPortfolio / customers.Count(),
					Churns = analytics.Churns,
					Registration = analytics.Registration,
					TotalCustomer = customers.Count(),
					TotalPortfolio = totalPortfolio,
					UserId = analytics.UserId,
					Meetings = meetings,
					NoShows = noShows,

				};

				return analyticsViewModel;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}



		}

		public void InsertAnalytics(AnalyticsViewModel analytics)
		{
			var token = _section.GetUserSection();
			var user = TokenService.GetDataInToken(token);

			analytics.NoShows.UserId = user.Id;
			analytics.NoShows.Registration = DateTime.Now;
			_context.NoShows.Add(analytics.NoShows);
			analytics.Meetings.UserId = user.Id;
			analytics.Meetings.Registration = DateTime.Now;
			_context.Meetings.Add(analytics.Meetings);


			AnalyticsModel newAnalytics = new AnalyticsModel
			{
				AverageTicket = analytics.AverageTicket,
				UserId = user.Id,
				TotalPortfolio = analytics.TotalPortfolio,
				TotalCustomer = analytics.TotalCustomer,
				Registration = DateTime.Now,
				Churns = analytics.Churns,

			};


			_context.Analytics.Add(newAnalytics);
			_context.SaveChanges();
		}

		public AnalyticsViewModel GetNewEmptyAnalytics()
		{

			*//*MeetingsMonths _meets = new MeetingsMonths()
			{
				Id = Guid.NewGuid(),
				UserId = Guid.NewGuid(),
				Registration = DateTime.Now,
				Segunda = "12",
				Terca = "5",
				Quarta = "12",
				Quinta = "8",
				Sexta = "13"


			};
			NoShowsMonth _noShows = new NoShowsMonth
			{
				Id = Guid.NewGuid(),
				UserId = Guid.NewGuid(),
				Registration = DateTime.Now,
				Segunda = "4",
				Terca = "11",
				Quarta = "7",
				Quinta = "13",
				Sexta = "5"
			};*//*
			AnalyticsViewModel _analytics = new AnalyticsViewModel
			{
				AverageTicket = 0,
				Churns = 0,
				Registration = DateTime.Now,
				TotalCustomer = 0,
				TotalPortfolio = 0,
				UserId = Guid.NewGuid()
				*//*Meetings = _meets,
				NoShows = _noShows,*//*
			};
			return _analytics;
		}
	}
}
*/