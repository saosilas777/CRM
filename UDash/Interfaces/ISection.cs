namespace CRM.Interfaces
{
	public interface ISection
	{
		void UserSectionCreate(string token);
		void UserSectionRemove();
		string GetUserSection();
	}
}
