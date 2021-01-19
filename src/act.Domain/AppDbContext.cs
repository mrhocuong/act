using act.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace act.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .ToTable(nameof(Customer))
                .HasKey(w => w.Id);
            builder.Entity<Customer>().Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Entity<Transaction>()
                .ToTable(nameof(Transaction))
                .HasKey(w => w.Id);
            builder.Entity<Transaction>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<Transaction>().Property(x => x.Amount).HasColumnType("decimal(18,2)");
        }
    }
}