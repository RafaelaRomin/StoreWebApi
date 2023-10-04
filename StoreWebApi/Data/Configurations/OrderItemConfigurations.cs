using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebApi.Models;

namespace StoreWebApi.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.Property(x => x.Quantity).HasDefaultValue(1).IsRequired();
            builder.Property(x => x.Total).HasColumnType("DECIMAL(18,2)").IsRequired();
        }
    }
}
