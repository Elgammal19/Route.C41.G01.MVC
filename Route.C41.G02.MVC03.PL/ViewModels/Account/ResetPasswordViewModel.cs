//using System.ComponentModel.DataAnnotations;

//namespace Route.C41.G02.MVC03.PL.ViewModels.Account
//{
//    public class ResetPasswordViewModel
//    {
//        [Required(ErrorMessage = "Password Is Required")]
//        [MinLength(6, ErrorMessage = "Minimum Password Length Is 6")]
//        [DataType(DataType.Password)]
//        public string NewPassword { get; set; }

//        [Required(ErrorMessage = "ConfirmPassword Is Required")]
//        [MinLength(6, ErrorMessage = "Minimum ConfirmPassword Length Is 6")]
//        [DataType(DataType.Password)]
//        [Compare(nameof(NewPassword), ErrorMessage = "ConfirmPassword Doesn't Match Password")]
//        public string ConfirmPassword { get; set; }
//    }
//}
