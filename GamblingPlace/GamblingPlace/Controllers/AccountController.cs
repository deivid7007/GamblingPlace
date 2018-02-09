using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamblingPlace.DTO;
using GP.Common.Helpers;
using GP.UserService;
using GP.UserService.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamblingPlace.Controllers
{
    public class AccountController : Controller
    {
        private IUser _userManager = new UserManager();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterEntry entry)
        {
            var password = HashUtils.CreateHashCode(entry.Password);

            User user = new User(entry.Email,password,false);
            

            if (user.Email != null && user.Password != null)
            {
                var existOrNot = _userManager.CheckEmailForExistance(user.Email);

                if (existOrNot == null)
                {
                    _userManager.RegisterAsync(user);
                }
                else
                {
                    return RedirectToAction("About", "Home"); // Must implement other redirection for error
                }
            }

            else
            {
                return RedirectToAction("About", "Home"); // Must implement other redirection for error
            }

            return RedirectToAction("Index", "Home");
        }
    }
}