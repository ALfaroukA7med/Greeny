using Greeny.BLL.ModelVM.AuthVM;
using Greeny.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public async Task<IActionResult> Index()
        {
            var roles = await Task.FromResult(_roleManager.Roles.ToList());

            return View(roles);
        }

        public IActionResult AddRole()
        {
            return View("AddRole");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleVM vm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role=new IdentityRole();
                role.Name = vm.RoleName.ToString();
                IdentityResult result= await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    ViewBag.success = "Add Role Successfully";
                    return View("AddRole");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("AddRole",vm);
        }


        public IActionResult DeleteRole()
        {
            return View("DeleteRole");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    TempData["Error"] = "Cannot delete role (maybe assigned to users)";
                }
            }

            return RedirectToAction("Index","User");
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RegisterWithRole()
        {
            var roles = await _roleManager.Roles
                .Select(r => r.Name)
                .ToListAsync();

            RegisterWithRoleVM vm = new RegisterWithRoleVM()
            {
                Roles = roles
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRegisterWithRole(RegisterWithRoleVM vm)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(vm.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email is already in use");
                    return View("RegisterWithRole", vm);
                }

                User user = new User
                {
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    UserName = vm.Email
                  
                };
                user.EmailConfirmed = true;
                user.OTP = "0000000";
                IdentityResult result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, vm.RoleName.ToString());
                    ViewBag.Success = "User created successfully";
                    return View("RegisterWithRole", vm);
                }
                
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("RegisterWithRole", vm);
        }



    }
}
