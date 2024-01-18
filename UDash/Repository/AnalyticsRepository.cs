using Microsoft.EntityFrameworkCore;
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
		private readonly Context _context;

		public AnalyticsRepository(Context context)
		{
			_context = context;
		}

		public AnalyticsModel GetAnalytics()
		{
			try
			{
				var analytics = _context.Analytics.OrderByDescending(x => x.Registration).LastOrDefault();
				if(analytics == null)
				{
					return  new AnalyticsModel();
				}
				return  analytics;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}
	}
}
