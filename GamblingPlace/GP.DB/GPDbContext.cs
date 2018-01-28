using GP.UserService.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace GP.DB
{
    public class GPDbContext : DbContext
    {
        public GPDbContext()
        {
            
        }

        public DbSet<User> Users { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:gamblingplace.database.windows.net,1433;Initial Catalog=gamblingplace;Persist Security Info=False;User ID=gp-admin;Password=Ludaka!234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
