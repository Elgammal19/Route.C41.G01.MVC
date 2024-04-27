using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.MVC03.PL.ViewModels.Account;
using System.Threading.Tasks;

namespace Route.C41.G02.MVC03.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser>  userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region Sign Up

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
            if(ModelState.IsValid)
            {
                var User = await _userManager.FindByNameAsync(model.Username);

                if (User is null)
                {
                    User = await _userManager.FindByEmailAsync(model.Email);

                    if(User is null)
                    {
						User = new ApplicationUser()
						{
							FName = model.FirstName,
							LName = model.LastName,
							UserName = model.Username,
							Email = model.Email,
							IsAgree = model.IsAgree,
						};

						var Result = await _userManager.CreateAsync(User, model.Password);

						if (Result.Succeeded)
							return RedirectToAction(nameof(SignIn));

						foreach (var error in Result.Errors)
							ModelState.AddModelError(string.Empty, error.Description);
					}
                }

                ModelState.AddModelError(string.Empty, "This User Is Already In Use For Another Account");

			}
			return View(model);
		}

        #endregion


        #region Sign In

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);

                if(User is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(User, model.Password);

                    if (flag) 
                    { 
                        var result = await _signInManager.PasswordSignInAsync(User, model.Password , model.RememberMe ,false);

						if (result.IsNotAllowed)
							ModelState.AddModelError(string.Empty, "Your Email n't confirmed yet !!");

						if (result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Email is locked !!");

						if (result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index) , "Home");

                        
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(model); 
        }

		#endregion
	}
}
