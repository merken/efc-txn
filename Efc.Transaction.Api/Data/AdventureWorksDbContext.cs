using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Efc.Transaction.Api.Data
{
    public class AdventureWorksDbContext : DbContext
    {
        private readonly DbConnection connection;
        public AdventureWorksDbContext(DbConnection connection)
        {
            this.connection = connection;
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(this.connection);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("DimCustomer");

            modelBuilder.Entity<Customer>()
                .HasKey("CustomerKey");
        }

    }
}