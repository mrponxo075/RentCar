using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;

namespace RentCar.Infrastructure.Repositories
{
    public class RentCarContext(DbContextOptions<RentCarContext> options) : DbContext(options)
    {
        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<FuelType> FuelTypes { get; set; }

        public virtual DbSet<IDCardType> IDCardTypes { get; set; }

        public virtual DbSet<Model> Models { get; set; }

        public virtual DbSet<Rental> Rentals { get; set; }

        public virtual DbSet<Tax> Taxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .HasIndex(c => c.ModelId)
                .IsUnique();
        }
    }
}
