using Microsoft.EntityFrameworkCore;

namespace ShopManagement.Models
{
    public class ShopDB : DbContext
    {
        public ShopDB(DbContextOptions<ShopDB> options) : base(options)
        {

        }

        public DbSet<Counter> Counters { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasOne(x => x.Counter).WithMany(x => x.Customers)
                .HasForeignKey(x => x.CounterId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Counter>().HasData(
                new Counter() { CounterId = 1, PosName = "Mr. One"},
                new Counter() { CounterId = 2, PosName = "Mr. Two"},
                new Counter() { CounterId = 3, PosName = "Mr. Three" }
                );
        }
    }
}
