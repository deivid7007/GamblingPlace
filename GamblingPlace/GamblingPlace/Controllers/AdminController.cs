using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamblingPlace.DTO;
using GamblingPlace.Extensions;
using GP.DB;
using GP.LogService;
using GP.LogService.Domain;
using GP.UserService;
using GP.UserService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GamblingPlace.Controllers
{
    public class AdminController : Controller
    {
        private GPDbContext _ctx = new GPDbContext();
        private IUser _userManager = new UserManager();
        private IAdmin _adminManager = new AdminManager();
        private ILog _logger = Logger.GetInstance;
        private string _email;
        private string _userId;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCoinsByEmail()
        {
            ViewData["Message"] = "Your contact page.";
            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            _email = HttpContext.Session.GetObjectFromJson<string>("Email");
            ViewData["UserId"] = _userId;
            ViewData["Email"] = _email;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCoinsByEmail(CoinsEntry entry)
        {
            try
            {
                User user = await _userManager.CheckEmailForExistance(entry.Email);

                if (user != null)
                {
                    await _adminManager.AddCoinsByEmail(entry.Email,entry.Coins);
                }
                
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("AddCoinsByEmail", "Admin");
        }

    }
}