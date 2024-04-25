using System.ComponentModel.DataAnnotations;

namespace Route.C41.G02.MVC03.PL.ViewModels.Account
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="First Name Is Required")]
		[Display(Name ="First Name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name Is Required")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Username Is Required")]
		public string Username { get; set; }

		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress(ErrorMessage ="Email Is Invalid")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[MinLength(6 , ErrorMessage ="Minimum Password Length Is 6")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword Is Required")]
		[MinLength(6, ErrorMessage = "Minimum ConfirmPassword Length Is 6")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password) , ErrorMessage = "ConfirmPassword Doesn't Match Password")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }


	}
}
