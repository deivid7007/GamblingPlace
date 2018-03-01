using GP.DB;
using GP.RouletteService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GP.RouletteService
{
    public class RouletteManager : IRoulette
    {
        private readonly GPDbContext _ctx;

        public RouletteManager()
        {
            this._ctx = new GPDbContext();
        }

        public Task BetCoinsOnBlack(double coins)
        {
            throw new NotImplementedException();
        }

        public Task BetCoinsOnRed(double coins)
        {
            throw new NotImplementedException();
        }
    }
}
