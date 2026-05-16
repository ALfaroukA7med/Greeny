using Greeny.BLL.ModelVM.AuthVM;
using Greeny.BLL.Services.Interfaces;
using Greeny.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Greeny.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _emailService = emailService;
        }


        public IActionResult Index()
        {
            return View("Home", "Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterVM vm)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(vm.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email is already in use");
                    return View("Register", vm);
                }
                var otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
                User user = new User
                {
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    UserName = vm.Email,
                    OTP = otp
                };

                IdentityResult result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    //await _signInManager.SignInAsync(user, false);

                    var path = Path.Combine(_env.WebRootPath, "Templates", "EmailConfirme.html");
                    var htmlBody = await System.IO.File.ReadAllTextAsync(path);
                    htmlBody = htmlBody.Replace("{{Name}}", user.FirstName);
                    htmlBody = htmlBody.Replace("{{OTP}}", otp);
                    await _emailService.SendEmailAsync(vm.Email, "Confirme Email", htmlBody);

                    return RedirectToAction("ConfirmeEmail", new {Email =vm.Email});
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", vm);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLogin(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null && user.IsDeleted != true)
                {
                    bool checkPassword = await _userManager.CheckPasswordAsync(user, vm.Password);
                    if (checkPassword)
                    {
                        await _signInManager.SignInAsync(user, vm.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Email or Password Wrong");
            }
            return View("Login", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }




        [HttpGet]
        public IActionResult ConfirmeEmail()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveConfirmeEmail(ConfirmeEmailVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View("ConfirmeEmail", vm);
                }

                if (string.IsNullOrEmpty(user.OTP))
                {
                    ModelState.AddModelError("", "OTP expired or already used");
                    return View("ConfirmeEmail", vm);
                }

                if (user.OTP != vm.OTP)
                {
                    ModelState.AddModelError("", "Invalid OTP");
                    return View("ConfirmeEmail", vm);
                }

                user.EmailConfirmed = true;
                user.OTP = "000000";

                await _userManager.UpdateAsync(user);

                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Login");
            }
            return View("ConfirmeEmail",vm);
        }



        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailForPassword(ForgetPasswordVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View("ForgetPassword", vm);
                }
                var otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
                user.OTP = otp;
                await _userManager.UpdateAsync(user);
                var path = Path.Combine(_env.WebRootPath, "Templates", "ForgetPassword.html");
                var htmlBody = await System.IO.File.ReadAllTextAsync(path);
                htmlBody = htmlBody.Replace("{{Name}}", user.FirstName);
                htmlBody = htmlBody.Replace("{{OTP}}", otp);
                await _emailService.SendEmailAsync(vm.Email, "Reset Password", htmlBody);

                return RedirectToAction("AddNewPassword");
            }
            return View("ForgetPassword",vm);
        }

        [HttpGet]
        public IActionResult AddNewPassword()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> SaveNewPassword(AddNewPasswordVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View("AddNewPassword", vm);
                }
                if (user.OTP != vm.OTP)
                {
                    ModelState.AddModelError("", "Invalid OTP");
                    return View("AddNewPassword", vm);
                }
                user.OTP = "000000";
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, vm.Password);
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Login");
            }
            return View("AddNewPassword",vm);
        }





    }
}
