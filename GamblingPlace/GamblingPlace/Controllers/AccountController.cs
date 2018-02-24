using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamblingPlace.DTO;
using GP.Common.Helpers;
using GP.LogService;
using GP.LogService.Domain;
using GP.UserService;
using GP.UserService.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamblingPlace.Controllers
{
    public class AccountController : Controller
    {
        private IUser _userManager = new UserManager();
        private ILog _logger = Logger.GetInstance;


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
        public async Task<IActionResult> Register(RegisterEntry entry)
        {
            try
            {

                var password = HashUtils.CreateHashCode(entry.Password);

                User user = new User(entry.Email, password, false);

                if (user.Email != null && user.Password != null)
                {
                    var existOrNot = await _userManager.CheckEmailForExistance(user.Email);

                    if (existOrNot == null)
                    {
                        await _userManager.RegisterAsync(user);
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
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}