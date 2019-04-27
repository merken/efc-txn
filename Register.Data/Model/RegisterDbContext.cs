using Microsoft.EntityFrameworkCore;

namespace Register.Data.Model
{
    public interface IMigrationContext
    {
        //This is used when applying migrations
    }

    public class RegisterDbContext : DbContext, IMigrationContext
    {
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<BankTransfer> BankTransfer { get; set; }

        public RegisterDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .ToTable("Members")
                .HasKey(m => m.Id);

            modelBuilder.Entity<Shipment>()
                .ToTable("Shipments")
                .HasKey(s => s.Id);

            modelBuilder.Entity<BankTransfer>()
                .ToTable("BankTransfers")
                .HasKey(b => b.Id);

            modelBuilder.Entity<Member>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Shipment>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BankTransfer>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            //EF Core 2.1 Value Conversion
            modelBuilder.Entity<Shipment>()
                .Property(s => s.Type)
                .HasConversion<string>();

            modelBuilder.Entity<BankTransfer>()
                .Property(b => b.TransferRecurrency)
                .HasConversion<string>();

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Member)
                .WithMany()
                .HasForeignKey(s => s.MemberId);

            modelBuilder.Entity<BankTransfer>()
                .HasOne(b => b.Member)
                .WithMany()
                .HasForeignKey(b => b.MemberId);

            //EF Core 2.1 Owned Type
            modelBuilder.Entity<Shipment>()
                .OwnsOne(s => s.Address,
                address =>
                {
                    address.Property(a => a.Street).HasColumnName("Street");
                    address.Property(a => a.StreetNumber).HasColumnName("StreetNumber");
                    address.Property(a => a.City).HasColumnName("City");
                    address.Property(a => a.State).HasColumnName("State");
                    address.Property(a => a.Country).HasColumnName("Country");
                });
        }

    }
}