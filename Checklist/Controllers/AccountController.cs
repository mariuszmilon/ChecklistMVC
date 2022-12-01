using Checklist.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Checklist.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext?.User;
            var name = currentUser.Identity.Name;
            if(name is null)
                return View();

            return RedirectToAction("Index", "Assignment");    
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Assignment");
                }
                return View(loginViewModel);
            }
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
                return View(registerViewModel);

            var user1 = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user1 != null)
            {
                TempData["Error"] = "This email address is already taken!";
                return View(registerViewModel);
            }

            var user2 = await _userManager.FindByNameAsync(registerViewModel.Username);
            if (user2 != null)
            {
                TempData["Error"] = "This nick is already taken!";
                return View(registerViewModel);
            }

            User newUser = new User();
            newUser.UserName = registerViewModel.Username;
            newUser.Email = registerViewModel.Email;
            newUser.Name = registerViewModel.Name;

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if(!newUserResponse.Succeeded)
            {
                TempData["Error"] = "Passwords must have at least one non alphanumeric character, one digit and one uppercase!";
                return View(registerViewModel);
            }

            await _signInManager.SignInAsync(newUser, false);
            return RedirectToAction("Index", "Assignment");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
