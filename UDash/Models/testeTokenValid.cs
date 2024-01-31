using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
	public class testeTokenValid
	{
		[Key]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Token { get; set; } = string.Empty;
		public bool IsValid { get; set; }
	}
}
