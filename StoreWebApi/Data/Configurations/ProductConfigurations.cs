using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebApi.Models;

namespace StoreWebApi.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.BarCode).HasColumnType("VARCHAR(40)").IsRequired();
            builder.Property(p => p.ProductType).HasConversion<string>();
            builder.Property(p => p.Price).HasColumnType("DECIMAL(18,2)").IsRequired();
            builder.Property (p => p.Description).HasColumnType("VARCHAR(100)").IsRequired();
        }
    }
}
