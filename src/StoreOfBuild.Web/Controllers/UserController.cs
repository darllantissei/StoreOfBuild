using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreOfBuild.Domain.Account;
using StoreOfBuild.Web.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreOfBuild.Web.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class UserController : Controller
    {
        private readonly IManager _manager;

        public UserController(IManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var users = _manager.ListAll();
            var userViewModel = users.Select(u => new UserViewModel { Id = u.Id, Email = u.Email });
            return View(userViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            await _manager.CreateAsync(model.Email, model.Password, model.Role);
            return Redirect("~/User/Index");
        }
    }
}
