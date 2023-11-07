using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.dboContext
{
    public class dbContext : DbContext
    {
        public dbContext()
        {
                
        }
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: $"Rebate{Guid.NewGuid()}");

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rebate> Rebates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Product
            modelBuilder.Entity<Product>()
              .HasData(
                new Product { Id = 1, Identifier = "P1", Price = 1, Uom = "Feet", SupportedIncentives = SupportedIncentiveType.FixedCashAmount },
                new Product { Id = 2, Identifier = "P2", Price = 1, Uom = "Pounds", SupportedIncentives = SupportedIncentiveType.AmountPerUom },
                new Product { Id = 3, Identifier = "P3", Price = 1, Uom = "Gallons", SupportedIncentives = SupportedIncentiveType.FixedRateRebate }
                );

            //Rebate
            modelBuilder.Entity<Rebate>()
              .HasData(
                    new Rebate { Identifier = "R1", Amount = 1030, Percentage = 20m, Incentive = IncentiveType.FixedRateRebate },
                    new Rebate { Identifier = "R2", Amount = 5020, Percentage = 10m, Incentive = IncentiveType.FixedCashAmount },
                    new Rebate { Identifier = "R3", Amount = 6000, Percentage = 50m, Incentive = IncentiveType.AmountPerUom }
                );

        }

        //public dbContext(DbContextOptions<dbContext> options) : base(options)
        //{
        //}

    }
}
