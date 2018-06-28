using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using University.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace University.Controllers
{
    public class RoleController : Controller
    {

        private RoleManager<ApplicationRole> _roleManager;       
        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
           
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<ApplicationRoleViewModels> model = new List<ApplicationRoleViewModels>();
            model = _roleManager.Roles.Select(r => new ApplicationRoleViewModels
            {
                RoleName = r.Name,
                Id = r.Id,
                Description = r.Description,
                NumberOfUsers = r.Name.Count()
            }).ToList();
            ViewBag.ViewModels = model;
            return View();
        }

        public async Task<IActionResult> AddEditViewModels(ApplicationRoleViewModels applicationRoleViewModels)
        {
            
                bool isExist = !String.IsNullOrEmpty(applicationRoleViewModels.Id);
                ApplicationRole applicationRole = isExist ? await _roleManager.FindByIdAsync(applicationRoleViewModels.Id) :
               new ApplicationRole
               {
                   CreatedDate = DateTime.Now
               };               
                applicationRole.Name = applicationRoleViewModels.RoleName;
                applicationRole.Description = applicationRoleViewModels.Description;
                applicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                IdentityResult roleRuslt = isExist ? await _roleManager.UpdateAsync(applicationRole)
                                                    : await _roleManager.CreateAsync(applicationRole);
            
            return RedirectToAction("Index", "Role");
        }
        public async Task<IActionResult> DeleteApplicationRole(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(Id);
                if (applicationRole != null)
                {
                    IdentityResult roleRuslt = _roleManager.DeleteAsync(applicationRole).Result;
                    if (roleRuslt.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

    }
}
