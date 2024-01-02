using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using CRM.Repository;
using CRM.Services;

namespace CRM.ViewComponents
{
	public class Header : ViewComponent
	{
		private readonly SendFileImageRepository _sendFileImage;

		public Header(SendFileImageRepository sendFileImage)
		{
			_sendFileImage = sendFileImage;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				string? UserSection = HttpContext.Session.GetString("Token");

				if (string.IsNullOrEmpty(UserSection))
				{
					return null;
				}
				UserModel user = TokenService.GetDataInToken(UserSection);
				var imageModel = _sendFileImage.GetById(user.Id);
				UserViewModel userView = new UserViewModel();

				if (imageModel == null)
				{					
					userView.User = user;
					userView.Image = new SendFileImageModel()
					{
						Id = user.Id,
						Url = "",
						UserId = user.Id,
					};
					return View(userView);
				}
				userView.User = user;
				
				userView.Image = imageModel;


				return View(userView);
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}

		}


	}
}
