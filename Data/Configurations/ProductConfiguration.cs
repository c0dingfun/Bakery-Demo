using Bakery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        // HH: Using Fluent API to configure the EntityFramework (Core) on how to 
        // associate the Model (Product) and the Database table (Products)
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // HH: tell entity framework that Product.ImageName is mapped to 
            // the Products table's ImageFileName column
            builder.Property(p => p.ImageName).HasColumnName("ImageFileName");
        }
    }
}