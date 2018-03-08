using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamblingPlace.DTO;
using GamblingPlace.Extensions;
using GP.DB;
using GP.RouletteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamblingPlace.Controllers
{
    
    public class RouletteController : Controller
    {
        private string _userId;
        public string _email;
        private double _coins;
        private RouletteManager _rouletteManager = new RouletteManager();
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
        public async Task<IActionResult> Roulette(BetEntry entry)
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

            const int fullCicle = 360;
            int num = entry.RandomFromHidden;
            string color = entry.Color;
            int bettedCoins = entry.Input;

            if (num > fullCicle)
            {
                while (num>fullCicle)
                {
                    num -= fullCicle;
                }
            }

            double resultFloating = num / 24;

           double result = Math.Ceiling(resultFloating) + 1;

            if (color == "red")
            {
                if ((result - 1) == 0)
                {
                   await _rouletteManager.SaveCoinsOnLoose(_email,bettedCoins);
                }
                else if (((result - 1) % 2) == 1)
                {
                    await _rouletteManager.SaveCoinsOnLoose(_email, bettedCoins);
                    
                }
                else if (((result - 1) % 2) == 0)
                {
                    bettedCoins = bettedCoins * 2;

                    await _rouletteManager.SaveCoinsOnWin(_email, bettedCoins);
                }
            }

            else if (color == "green")
            {
                if ((result - 1) == 0)
                {
                    bettedCoins = bettedCoins * 14;

                    await _rouletteManager.SaveCoinsOnWin(_email, bettedCoins);
                }
                else if (((result - 1) % 2) == 1)
                {
                    await _rouletteManager.SaveCoinsOnLoose(_email, bettedCoins);
                }
                else if (((result - 1) % 2) == 0)
                {
                    await _rouletteManager.SaveCoinsOnLoose(_email, bettedCoins);
                }
            }

            else if (color == "black")
            {
                if ((result - 1) == 0)
                {
                    await _rouletteManager.SaveCoinsOnLoose(_email, bettedCoins);
                }
                else if (((result - 1) % 2) == 1)
                {
                    bettedCoins = bettedCoins * 2;

                    await _rouletteManager.SaveCoinsOnWin(_email, bettedCoins);
                    
                }
                else if (((result - 1) % 2) == 0)
                {
                    await _rouletteManager.SaveCoinsOnLoose(_email, bettedCoins);
                }
            }
            


            return RedirectToAction("Roulette", "Roulette");
        }

        public double UpdateCoins(string _userId)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.Id == _userId);
            return user.Coins;
        }




    }
}