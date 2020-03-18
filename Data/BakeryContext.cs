using Bakery.Data.Configurations;
using Bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Data
{
    public class BakeryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            opt.UseSqlite(@"Data source=Bakery.db");
        }

        // HH: ProductConfiguration is register here (OnModelCreating())
        protected override void OnModelCreating(ModelBuilder builder) => 
            builder.ApplyConfiguration(new ProductConfiguration())
                   .Seed(); // HH: calling extension method to seed the data
    }

}