using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamblingPlace.DTO;
using GamblingPlace.Extensions;
using GP.Common.Helpers;
using GP.DB;
using GP.LogService;
using GP.LogService.Domain;
using GP.NotificationService;
using GP.NotificationService.Domain;
using GP.UserService;
using GP.UserService.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamblingPlace.Controllers
{
    public class AccountController : Controller
    {
        private IUser _userManager = new UserManager();
        private ILog _logger = Logger.GetInstance;
        private INotificationActor _notificationManager = new NotificationManager();
        private GPDbContext _ctx = new GPDbContext();


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
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
                string validationCode = HashUtils.CreateReferralCode();

                User user = new User(entry.Email, password, validationCode);

                if (user.Email != null && user.Password != null)
                {
                    var existOrNot = await _userManager.CheckEmailForExistance(user.Email);

                    if (existOrNot == null)
                    {
                        await _userManager.RegisterAsync(user);
                        await _notificationManager.SendConfirmationEmailAsync(user);
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ValidateEmail(string userId, string validationCode)
        {
            if (userId == null || validationCode == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            try
            {
                User user = await _userManager.GetUserByIdAsync(userId);
                if (user == null || validationCode != user.ValidationCode)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                user = UpdateUserEmailConfirmation(user);
                _ctx.Update(user);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return RedirectToAction("Error", "Home");
            }
            return View("ConfirmEmail");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.SetObjectAsJson<string>("UserId", null);
            HttpContext.Session.SetObjectAsJson<string>("Email", null);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        private User UpdateUserEmailConfirmation(User user)
        {
            var updatedUser = new User(
                user.Email,
                user.Password,
                user.ValidationCode,
                false,
                true,
                user.Id);
            return updatedUser;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginEntry entry)
        {
            ViewData["WrongLogin"] = null;
            if (entry.Email == null || entry.Password == null)
            {
                ViewData["WrongLogin"] = "Incorrect form!";
                return View();
            }

            try
            {
                var user = await _userManager.GetUserByEmailAsync(entry.Email);
                if (user != null)
                {
                    if (!user.IsEmailConfirmed)
                    {
                        ViewData["WrongLogin"] = "Email is not confirmed!";
                        return View();
                    }
                    if (!HashUtils.VerifyPassword(entry.Password, user.Password))
                    {
                        /* Don't reveal which one is incorrect.  */
                        ViewData["WrongLogin"] = "Incorrect username or password!";
                        return View();

                    }
                }

                HttpContext.Session.SetObjectAsJson<string>("UserId", user.Id);
                HttpContext.Session.SetObjectAsJson<string>("Email", user.Email);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return RedirectToAction("Error", "Home");
            }


            return RedirectToAction("AllExceptions", "Home");
        }


    }
}