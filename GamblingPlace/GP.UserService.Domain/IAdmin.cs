using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GP.UserService.Domain
{
    public interface IAdmin
    {
        Task AddCoinsByEmail(string email,double coinsToAdd);
    }
}
