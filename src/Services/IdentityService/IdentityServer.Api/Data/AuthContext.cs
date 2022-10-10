using IdentityServer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Api.Data
{
    public class AuthContext : DbContext
    {
        public IConfiguration configuration => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuthContext"));
        }
    }
}
