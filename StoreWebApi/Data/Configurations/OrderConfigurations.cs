using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebApi.Models;

namespace StoreWebApi.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OpenedIn).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(o => o.OrderStatus).HasConversion<string>();
            builder.Property(o => o.Freight).HasConversion<int>();
            builder.Property(o => o.Comments).HasColumnType("VARCHAR(300)");

            builder.HasMany(o => o.Items)
                .WithOne(o => o.Order)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
