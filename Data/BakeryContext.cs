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
    }
}