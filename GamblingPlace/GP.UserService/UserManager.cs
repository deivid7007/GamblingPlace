﻿using GP.DB;
using GP.UserService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GP.UserService
{
    public class UserManager : IUser
    {
        private readonly GPDbContext _ctx;

        public UserManager()
        {
            this._ctx = new GPDbContext();
        }

        public async Task<User> CheckEmailForExistance(string email)
        {
            List<User> users = await _ctx.Users.ToListAsync();
            var result = users.FirstOrDefault(u => u.Email == email);
            return result;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _ctx.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            List<User> users = await _ctx.Users.ToListAsync();
            return users.FirstOrDefault(u => u.Id == id);
        }

        public Task LoginAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

       
    }
}
