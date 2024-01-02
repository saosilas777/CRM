using CRM.Data;
using CRM.Models;

namespace CRM.Repository
{
	
	public class SendFileImageRepository
	{
		private readonly Context _context;

		public SendFileImageRepository(Context context)
		{
			_context = context;
		}

		public void SendFileImage(SendFileImageModel fileImage)
		{
			_context.Add(fileImage);
			_context.SaveChanges();
		}
		public SendFileImageModel GetById(Guid id)
		{
			var image = _context.ImageUrl.FirstOrDefault(x => x.UserId == id);
			return image;
		}


	}
}
