using Microsoft.AspNetCore.Mvc;
using StoreOfBuild.Domain.Account;
using StoreOfBuild.Web.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreOfBuild.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthentication _authentication;

        public AccountController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authentication.Authenticate(model.Email, model.Password);

            if (result)
                return Redirect("/");
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid loggin attempt");
                return View(model);
            }
        }
    }
}
