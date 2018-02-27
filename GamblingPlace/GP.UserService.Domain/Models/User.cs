using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GP.UserService.Domain
{
    public sealed class User
    {
        public User()
        {

        }
        public User(string email, string password, string validationCode, bool isAdmin = false, bool isEmailConfirmed = false, string id = null)
        {
            this.Email = email;
            this.Password = password;
            this.Id = id ?? Guid.NewGuid().ToString();
            this.DateCreated = DateTime.Now;
            this.Coins = 10000d;
            this.IsAdmin = isAdmin;
            this.ValidationCode = validationCode;
            this.IsEmailConfirmed = isEmailConfirmed;
        }

        [Key]
        public string Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime DateCreated { get; private set; }
        public double Coins { get; set; }
        public bool IsAdmin { get; private set; }
        public string ValidationCode { get; private set; }
        public bool IsEmailConfirmed { get; private set; }






    }
}
