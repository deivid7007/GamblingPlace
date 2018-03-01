using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GP.RouletteService.Domain
{
    public interface IRoulette
    {
        Task BetCoinsOnRed(double coins);
        Task BetCoinsOnBlack(double coins);
    }
}
