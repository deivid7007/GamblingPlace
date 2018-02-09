using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GP.UserService.Domain
{
    public interface IUser
    {
        Task RegisterAsync(User user);
        Task LoginAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> CheckEmailForExistance(string email);
    }
}
