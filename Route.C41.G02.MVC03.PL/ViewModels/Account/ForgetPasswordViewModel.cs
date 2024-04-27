using System.ComponentModel.DataAnnotations;

namespace Route.C41.G02.MVC03.PL.ViewModels.Account
{
	public class ForgetPasswordViewModel
	{

		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Email Is Invalid")]
		public string Email { get; set; }
	}
}
