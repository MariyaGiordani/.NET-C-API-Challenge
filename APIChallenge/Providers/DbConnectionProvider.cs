using APIChallenge.Models;
using APIChallenge.Providers.ContextSettings;
using Microsoft.EntityFrameworkCore;

namespace APIChallenge.Providers
{
    public class DbConnectionProvider : DbContext
    {
        public DbConnectionProvider(DbContextOptions<DbConnectionProvider> options)
              : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Security> Security { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            //Apply User Configuration
            modelbuilder.ApplyConfiguration(new UserEntitySettings());
            modelbuilder.ApplyConfiguration(new SecurityEntitySettings());
        }
    }
}
