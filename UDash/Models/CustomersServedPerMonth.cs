using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
	public class CustomersServedPerMonth
	{
		[Key]
		public Guid Id { get; set; }
		public double? January { get; set; }
		public double? February { get; set; }
		public double? March { get; set; }
		public double? April { get; set; }
		public double? May { get; set; }
		public double? June { get; set; }
		public double? July { get; set; }
		public double? August { get; set; }
		public double? September { get; set; }
		public double? Octuber { get; set; }
		public double? November { get; set; }
		public double? December { get; set; }
		public UserModel? User { get; set; }
	}
}
