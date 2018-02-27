using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GamblingPlace.Extensions;
using Microsoft.AspNetCore.Mvc;
using GamblingPlace.Models;
using GP.DB;
using GP.LogService;
using GP.LogService.Domain;
using GP.LogService.Domain.Models;
using GP.UserService;
using GP.UserService.Domain;

namespace GamblingPlace.Controllers
{
    public class HomeController : Controller
    {
        private string _userId;
        private string _email;
        private double _coins;
        private IUser _userManager = new UserManager();
        private ILog _logger = Logger.GetInstance;
        private GPDbContext _ctx = new GPDbContext();

        public IActionResult Index()
        {
            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            _email = HttpContext.Session.GetObjectFromJson<string>("Email");
            _coins = HttpContext.Session.GetObjectFromJson<double>("Coins");
            ViewData["UserId"] = _userId;
            ViewData["Email"] = _email;
            ViewData["Coins"] = _coins;

            return View();
        }

        public IActionResult About()
        {
            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            _email = HttpContext.Session.GetObjectFromJson<string>("Email");
            _coins = HttpContext.Session.GetObjectFromJson<double>("Coins");
            ViewData["UserId"] = _userId;
            ViewData["Email"] = _email;
            ViewData["Coins"] = _coins;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            _email = HttpContext.Session.GetObjectFromJson<string>("Email");
            _coins = HttpContext.Session.GetObjectFromJson<double>("Coins");
            ViewData["UserId"] = _userId;
            ViewData["Email"] = _email;
            ViewData["Coins"] = _coins;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllExceptions()
        {
            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            _email = HttpContext.Session.GetObjectFromJson<string>("Email");
            _coins = HttpContext.Session.GetObjectFromJson<double>("Coins");
            ViewData["UserId"] = _userId;
            ViewData["Email"] = _email;
            ViewData["Coins"] = _coins;

            List<CustomException> exceptions = null;
            try
            {
                exceptions = _ctx.CustomExceptions
                    .OrderByDescending(e => e.DateCreated)
                    .Take(5)
                    .ToList();
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
            }

            return View(exceptions);
        }
    }
}
