using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GP.UserService.Domain;
using Microsoft.EntityFrameworkCore;

namespace GP.DB
{
    public static class DbInitializer
    {
        public static void Initialize(GPDbContext context)
        {
            context.Database.Migrate();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                // TODO: Can use foreach to add some few more users.
                context.Users.Add(new User("deivid7007@mail.bg", "ludaka1234", true));
                context.Users.Add(new User("kra4eto@domain.com", "123"));
                context.Users.Add(new User("doko@domain.com", "123"));
                context.Users.Add(new User("rafa@domain.com", "123"));
                context.Users.Add(new User("gosho@domain.com", "123"));
                context.SaveChanges();
            }

        }
    }
}
