using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP.DB;
using GP.UserService.Domain;
using Microsoft.EntityFrameworkCore;

namespace GP.UserService
{
    public class AdminManager : IAdmin
    {
        private readonly GPDbContext _ctx;

        public AdminManager()
        {
            this._ctx = new GPDbContext();
        }

        public async Task AddCoinsByEmail(string email, double coinsToAdd)
        {
            List<User> users = await _ctx.Users.ToListAsync();
            User user = _ctx.Users.FirstOrDefault(u => u.Email == email);
            user.Coins = user.Coins + coinsToAdd;
            await _ctx.SaveChangesAsync();
        }

    }
}
