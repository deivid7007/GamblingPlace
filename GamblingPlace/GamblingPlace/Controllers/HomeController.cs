using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private IUser _userManager = new UserManager();
        private ILog _logger = Logger.GetInstance;
        private GPDbContext _ctx = new GPDbContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> AllExceptions()
        {
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
