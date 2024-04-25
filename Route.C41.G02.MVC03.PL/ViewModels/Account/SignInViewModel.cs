using System.ComponentModel.DataAnnotations;

namespace Route.C41.G02.MVC03.PL.ViewModels.Account
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Email Is Invalid")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


        public bool  RememberMe { get; set; }



    }
}
