using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sarahah.Core.Domain.IdentityEntities;
using sarahah.Core.DTO;
using System.Security.Claims;

namespace sarahah.UI.Controllers
{
    [AllowAnonymous]
    [Route("[Controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("[action]")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }
            // Create an Object forthis aplication user class to create a new user 
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.Phone,
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded) // if user creation succeeded
            {

                await _signInManager.SignInAsync(user, false);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(registerDTO);




        }


        [HttpGet("[action]")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }
            var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password,  false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginDTO.UserName);
                await _signInManager.SignInAsync(user!, false);

                return RedirectToAction("GetAllMessages", "Message", new { id = user!.Id });
            }
            ModelState.AddModelError("", "Invalid Email or Password");
            return View(loginDTO);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true); // valid
            }
            else
            {
                return Json(false); // invalid
            }

        }

    }
}
