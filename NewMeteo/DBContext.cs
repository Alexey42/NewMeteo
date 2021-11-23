using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMeteo
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=NewMeteoDB;Integrated Security=false;Username=postgres;Password=0000");
        }
    }
}
