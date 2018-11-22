using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Register.Data.Model
{
    public class RegisterDbContextDesignTimeFactory : IDesignTimeDbContextFactory<RegisterDbContext>
    {
        public RegisterDbContext CreateDbContext(string[] args)
        {
            var connectionString = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build().GetConnectionString("RegisterDb");

            var options = new DbContextOptionsBuilder<RegisterDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;
            return new RegisterDbContext(options);
        }
    }
}