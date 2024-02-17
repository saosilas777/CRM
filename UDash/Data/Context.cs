using Microsoft.EntityFrameworkCore;
using CRM.Models;

namespace CRM.Data
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		public DbSet<UserModel> Users { get; set; }
		public DbSet<LoginModel> Login { get; set; }
/*		public DbSet<AnalyticsModel> Analytics { get; set; }*/
		public DbSet<_CustomerModel> Customers { get; set; }
		public DbSet<_EmailModel> Emails { get; set; }
		public DbSet<_PhoneModel> Phones { get; set; }
		public DbSet<Models._ContactRecords> ContactRecords { get; set; }
		public DbSet<SendFileImageModel> ImageUrl { get; set; }
		public DbSet<AnalyticsModel> Analytics { get; set; }
		





	}
}