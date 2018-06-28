using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Models;

namespace University.Controllers
{
    [Authorize]
    public class UserController : Controller
    {        
        private readonly UserManager<AppUser> _roleUserManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(UserManager<AppUser> roleUserManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _roleUserManager = roleUserManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Admin")]
        // GET: /<controller>/
        public IActionResult Index()
        {
            
            List<UserListView> model = new List<UserListView>();
            model = _roleUserManager.Users.Select(u => new UserListView
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();
            ViewBag.UserViewModels = model;
            ViewBag.userName = _signInManager.UserManager.GetUserName(User);
            return View();
        }
        
        public async Task<IActionResult> AddUser(User model)
        {
            if (ModelState.IsValid)
            {                               
                AppUser user = new AppUser
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Email = model.Email,
                    
                };
                IdentityResult result = await _roleUserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    AppRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _roleUserManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index", "User");
                        }
                    }
                }
            }
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                AppUser applicationUser = await _roleUserManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    IdentityResult result = await _roleUserManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
            }
            return RedirectToAction("Index", "User");
        }


        public async Task<IActionResult> EditUserView(string id, User model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _roleUserManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Email = model.Email;                    
                    IdentityResult result = await _roleUserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {                       
                         return RedirectToAction("Index", "User");                           
                    }
                }
            }
            return RedirectToAction("Index", "User");
        }





    }
}
