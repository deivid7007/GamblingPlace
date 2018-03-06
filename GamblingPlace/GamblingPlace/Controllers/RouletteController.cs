using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamblingPlace.DTO;
using GamblingPlace.Extensions;
using GP.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamblingPlace.Controllers
{
    
    public class RouletteController : Controller
    {
        private string _userId;
        private string _email;
        private double _coins;
        private GPDbContext _ctx = new GPDbContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Roulette()
        {
            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            _email = HttpContext.Session.GetObjectFromJson<string>("Email");
            if (_userId != null)
            {
                HttpContext.Session.SetObjectAsJson<double>("Coins", 0);
                double coins = UpdateCoins(_userId);
                HttpContext.Session.SetObjectAsJson<double>("Coins", coins);
            }
            _coins = HttpContext.Session.GetObjectFromJson<double>("Coins");
            ViewData["UserId"] = _userId;
            ViewData["Email"] = _email;
            ViewData["Coins"] = _coins;

            return View();
        }

        [HttpPost]
        public IActionResult Roulette([FromBody]BetEntry entry)
        {
            var num = entry.RandomFromHidden;

            return View();
        }

        public double UpdateCoins(string _userId)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.Id == _userId);
            return user.Coins;
        }




    }
}