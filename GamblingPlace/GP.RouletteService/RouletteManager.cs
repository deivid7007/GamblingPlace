using GP.DB;
using GP.RouletteService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP.UserService.Domain;
using Microsoft.EntityFrameworkCore;

namespace GP.RouletteService
{
    public class RouletteManager : IRoulette
    {
        private readonly GPDbContext _ctx;

        public RouletteManager()
        {
            this._ctx = new GPDbContext();
        }

        public async Task SaveCoins(string email,double coins)
        {
            List<User> users = await _ctx.Users.ToListAsync();
            User user = _ctx.Users.FirstOrDefault(u => u.Email == email);
            user.Coins = user.Coins + coins;
            await _ctx.SaveChangesAsync();
        }
    }
}
