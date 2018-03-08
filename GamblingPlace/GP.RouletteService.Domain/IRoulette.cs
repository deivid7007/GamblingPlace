using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GP.RouletteService.Domain
{
    public interface IRoulette
    {
        Task SaveCoinsOnWin(string email,double coins);
        Task SaveCoinsOnLoose(string email, double coins);
    }
}
