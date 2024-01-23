using CRM.Interfaces;

namespace CRM.Helpers
{
	public class Section : ISection
	{
		private readonly IHttpContextAccessor _httpContext;

		public Section(IHttpContextAccessor httpContext)
		{
			_httpContext = httpContext;
		}

		public string GetUserSection()
		{
			try
			{
				string userSection = _httpContext.HttpContext.Session.GetString("Token");
				if (string.IsNullOrEmpty(userSection)) return null;
				return userSection;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		public void UserSectionCreate(string token)
		{
			//string _user = JsonConvert.SerializeObject(user);
			_httpContext.HttpContext.Session.SetString("Token", token);
		}

		public void UserSectionRemove()
		{
			_httpContext.HttpContext.Session.Remove("Token");
		}
	}
}